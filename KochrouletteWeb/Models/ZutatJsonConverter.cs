using System.Text.Json;
using System.Text.Json.Serialization;

namespace KochrouletteWeb.Models
{
    public class ZutatJsonConverter : JsonConverter<Zutat>
    {
        public override Zutat Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string? name = null;
            Zutat.Kategorie art = default;
            bool isVegetarisch = true;

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject) break;

                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    string propName = reader.GetString()!;
                    reader.Read();

                    switch (propName.ToLower())
                    {
                        case "name":
                            name = reader.GetString();
                            break;
                        case "art":
                            art = Enum.Parse<Zutat.Kategorie>(reader.GetString()!, true);
                            break;
                        case "isvegetarisch":
                            isVegetarisch = reader.GetBoolean();
                            break;
                    }
                }
            }

            if (name == null)
                throw new JsonException("Name ist erforderlich.");

            return new Zutat(name, art, isVegetarisch);
        }

        public override void Write(Utf8JsonWriter writer, Zutat value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("name", value.Name);
            writer.WriteString("art", value.Art.ToString());
            writer.WriteBoolean("isVegetarisch", value.IsVegetarisch);
            writer.WriteEndObject();
        }
    }
}
