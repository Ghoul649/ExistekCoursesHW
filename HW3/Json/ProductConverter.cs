using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HW3
{
    class ProductConverter : JsonConverter<Product>
    {
        enum TypeDiscriminator
        {
            FlashDrive = 1,
            Drive = 2,
            OpticalDisc = 3
        }

        public override bool CanConvert(Type typeToConvert) =>
            typeof(Product).IsAssignableFrom(typeToConvert);

        public override Product Read(
            ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            reader.Read();
            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException();
            }

            string propertyName = reader.GetString();
            if (propertyName != "TypeDiscriminator")
            {
                throw new JsonException();
            }

            reader.Read();
            if (reader.TokenType != JsonTokenType.Number)
            {
                throw new JsonException();
            }

            TypeDiscriminator typeDiscriminator = (TypeDiscriminator)reader.GetInt32();
            Product p = null;

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return p;
                }

                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    propertyName = reader.GetString();
                    reader.Read();
                    if (p == null) 
                    {
                        if (propertyName != "ID")
                            throw new JsonException();
                        switch (typeDiscriminator) 
                        {
                            case TypeDiscriminator.FlashDrive:
                                p = new FlashDrive(reader.GetUInt32());
                                break;
                            case TypeDiscriminator.Drive:
                                p = new Drive(reader.GetUInt32());
                                break;
                            case TypeDiscriminator.OpticalDisc:
                                p = new OpticalDisc(reader.GetUInt32());
                                break;
                            default:
                                throw new JsonException();
                        }
                    }
                    switch (propertyName)
                    {
                        case "Name":
                            p.Name = reader.GetString();
                            break;
                        case "Model":
                            p.Model = reader.GetString();
                            break;
                        case "ProducerNamep":
                            p.ProducerName = reader.GetString();
                            break;
                        case "Price":
                            p.Price = reader.GetDouble();
                            break;
                        case "Count":
                            p.Count = reader.GetInt32();
                            break;
                        case "Capacity":
                            (p as StorageDevice).Capacity = new DataSize(reader.GetString());
                            break;
                        case "USBVersion":
                            (p as FlashDrive).USBVersion = reader.GetString();
                            break;
                        case "DriveType":
                            (p as Drive).DriveType = reader.GetString();
                            break;
                        case "WritingSpeed":
                            if (p is Drive)
                                (p as Drive).WritingSpeed = new DataSpeed(reader.GetString());
                            else
                                (p as OpticalDisc).WritingSpeed = reader.GetInt32();
                            break;
                        case "ReadingSpeed":
                            (p as Drive).ReadingSpeed = new DataSpeed(reader.GetString());
                            break;
                        case "":
                            (p as OpticalDisc).PackSize = reader.GetInt32();
                            break;
                    }
                }
            }

            throw new JsonException();
        }

        public override void Write(
            Utf8JsonWriter writer, Product p, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            if (p is FlashDrive)
                writer.WriteNumber("TypeDiscriminator", (int)TypeDiscriminator.FlashDrive);
            else if (p is Drive)
                writer.WriteNumber("TypeDiscriminator", (int)TypeDiscriminator.Drive);
            else if (p is OpticalDisc)
                writer.WriteNumber("TypeDiscriminator", (int)TypeDiscriminator.OpticalDisc);
            else throw new NotImplementedException();
            
            writer.WriteNumber("ID", p.ID);
            writer.WriteString("Name", p.Name);
            writer.WriteString("ProducerName", p.ProducerName);
            writer.WriteString("Model", p.Model);
            writer.WriteNumber("Count", p.Count);
            writer.WriteNumber("Price", p.Price);

            if (p is StorageDevice) 
            {
                writer.WriteString("Capacity", (p as StorageDevice).Capacity.ToString());
            }

            if (p is FlashDrive)
            {
                writer.WriteString("USBVersion", (p as FlashDrive).USBVersion);
            }
            else if (p is Drive)
            {
                writer.WriteString("DriveType", (p as Drive).DriveType);
                writer.WriteString("WritingSpeed", (p as Drive).WritingSpeed.ToString());
                writer.WriteString("ReadingSpeed", (p as Drive).ReadingSpeed.ToString());
            }
            else if (p is OpticalDisc)
            {
                writer.WriteNumber("WritingSpeed", (p as OpticalDisc).WritingSpeed);
                writer.WriteNumber("PackSize", (p as OpticalDisc).PackSize);
            }

            writer.WriteEndObject();
        }
    }
}
