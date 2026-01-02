#pragma once

#include <string>
#include <any>
#include <sstream>
#include <ostream>
#include <memory>
#include <unordered_map>
#include <vector>

namespace Taml
{
    /// <summary>
    /// Interface for TAML serializers responsible for serializing and deserializing TAML documents.
    /// </summary>
    class Serializer
    {
    public:
        /// <summary>
        /// Serializes an object to TAML format and returns a string
        /// </summary>
        static std::string Serialize(const std::any& obj);

        /// <summary>
        /// Serializes an object to TAML format and writes to a stream
        /// </summary>
        static void Serialize(const std::any& obj, std::ostream& stream);

        /// <summary>
        /// Serializes an object to TAML format and returns a MemoryStream
        /// </summary>
        static std::unique_ptr<std::stringstream> SerializeToStream(const std::any& obj);


    private:
        static constexpr char Tab = '\t';
        static constexpr char NewLine = '\n';

        static void SerializeObject(const std::any& obj, std::stringstream& sb, int depth);
        static void SerializeComplexObject(const std::any& obj, std::stringstream& sb, int indentLevel);
        static void SerializeMember(const std::string& name, const std::any& value, std::stringstream& sb, int indentLevel);
        static void WriteIndent(std::stringstream& sb, int indentLevel);
        static bool IsPrimitiveType(const std::type_info& type);
        static std::string FormatValue(const std::any& value);
        static void SerializeDictionary(const std::unordered_map<std::string, std::any>& dict, std::stringstream& sb, int indentLevel);
        static void SerializeCollection(const std::vector<std::any>& collection, std::stringstream& sb, int indentLevel);
    };
}