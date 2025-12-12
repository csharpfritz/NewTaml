using System.Text;
using TAML.Core;

namespace TAML.Tests;

public class TamlSerializerTests
{
    #region Primitive Types Tests
    
    [Fact]
    public void GivenNullObject_WhenSerializing_ThenReturnsNull()
    {
        // Given
        object? obj = null;
        
        // When
        var result = TamlSerializer.Serialize(obj!);
        
        // Then
        Assert.Equal("null", result);
    }
    
    [Fact]
    public void GivenSimpleString_WhenSerializing_ThenReturnsStringValue()
    {
        // Given
        var obj = "Hello World";
        
        // When
        var result = TamlSerializer.Serialize(obj);
        
        // Then
        Assert.Equal("Hello World", result);
    }
    
    [Fact]
    public void GivenInteger_WhenSerializing_ThenReturnsIntegerAsString()
    {
        // Given
        var obj = 42;
        
        // When
        var result = TamlSerializer.Serialize(obj);
        
        // Then
        Assert.Equal("42", result);
    }
    
    [Fact]
    public void GivenBoolean_WhenSerializing_ThenReturnsLowercaseTrueOrFalse()
    {
        // Given
        var trueValue = true;
        var falseValue = false;
        
        // When
        var trueResult = TamlSerializer.Serialize(trueValue);
        var falseResult = TamlSerializer.Serialize(falseValue);
        
        // Then
        Assert.Equal("true", trueResult);
        Assert.Equal("false", falseResult);
    }
    
    [Fact]
    public void GivenDecimal_WhenSerializing_ThenReturnsDecimalAsString()
    {
        // Given
        var obj = 3.14m;
        
        // When
        var result = TamlSerializer.Serialize(obj);
        
        // Then
        Assert.Equal("3.14", result);
    }
    
    #endregion
    
    #region Multi-Tab Separator Tests
    
    [Fact]
    public void GivenTamlWithSingleTab_WhenDeserializing_ThenParsesCorrectly()
    {
        // Given
        var taml = "name\tJohn\nage\t30";
        
        // When
        var result = TamlSerializer.Deserialize<SimpleObject>(taml);
        
        // Then
        Assert.NotNull(result);
        Assert.Equal("John", result.Name);
        Assert.Equal(30, result.Age);
    }
    
    [Fact]
    public void GivenTamlWithMultipleTabs_WhenDeserializing_ThenParsesCorrectly()
    {
        // Given
        var taml = "name\t\t\tJohn\nage\t\t30";
        
        // When
        var result = TamlSerializer.Deserialize<SimpleObject>(taml);
        
        // Then
        Assert.NotNull(result);
        Assert.Equal("John", result.Name);
        Assert.Equal(30, result.Age);
    }
    
    [Fact]
    public void GivenTamlWithMixedTabCounts_WhenDeserializing_ThenAllParsedCorrectly()
    {
        // Given
        var taml = "name\tAlice\nage\t\t\t25\nisActive\t\ttrue";
        
        // When
        var result = TamlSerializer.Deserialize<SimpleObject>(taml);
        
        // Then
        Assert.NotNull(result);
        Assert.Equal("Alice", result.Name);
        Assert.Equal(25, result.Age);
        Assert.True(result.IsActive);
    }
    
    [Fact]
    public void GivenSerializedObjectWithMultipleTabs_WhenRoundTripping_ThenDeserializesCorrectly()
    {
        // Given - Serialize an object first
        var original = new SimpleObject
        {
            Name = "TestName",
            Age = 42,
            IsActive = true
        };
        
        var serialized = TamlSerializer.Serialize(original);
        
        // Add extra tabs to the serialized output
        var withMultipleTabs = serialized
            .Replace("Name\tTestName", "Name\t\t\t\tTestName")
            .Replace("Age\t42", "Age\t\t\t42")
            .Replace("IsActive\ttrue", "IsActive\t\ttrue");
        
        // When - Deserialize both versions
        var result1 = TamlSerializer.Deserialize<SimpleObject>(serialized);
        var result2 = TamlSerializer.Deserialize<SimpleObject>(withMultipleTabs);
        
        // Then - Both should produce identical results
        Assert.NotNull(result1);
        Assert.NotNull(result2);
        Assert.Equal(result1.Name, result2.Name);
        Assert.Equal(result1.Age, result2.Age);
        Assert.Equal(result1.IsActive, result2.IsActive);
        Assert.Equal("TestName", result2.Name);
        Assert.Equal(42, result2.Age);
        Assert.True(result2.IsActive);
    }
    
    [Fact]
    public void GivenVisuallyAlignedTaml_WhenDeserializing_ThenParsesAsExpected()
    {
        // Given - simulating aligned columns for readability
        var taml = "short\t\t\tvalue1\n" +
                   "very_long_key\tvalue2\n" +
                   "medium\t\t\tvalue3";
        
        // When - Test with a simple object
        var result = TamlSerializer.Deserialize<AlignedObject>(taml);
        
        // Then
        Assert.NotNull(result);
        Assert.Equal("value1", result.Short);
        Assert.Equal("value2", result.Very_long_key);
        Assert.Equal("value3", result.Medium);
    }
    
    #endregion
    
    #region Simple Object Tests
    
    [Fact]
    public void GivenSimpleObject_WhenSerializing_ThenReturnsKeyValuePairs()
    {
        // Given
        var obj = new SimpleObject
        {
            Name = "Test",
            Age = 25,
            IsActive = true
        };
        
        // When
        var result = TamlSerializer.Serialize(obj);
        
        // Then
        var lines = result.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        Assert.Contains("Name\tTest", lines);
        Assert.Contains("Age\t25", lines);
        Assert.Contains("IsActive\ttrue", lines);
    }
    
    [Fact]
    public void GivenObjectWithNullProperty_WhenSerializing_ThenReturnsNullValue()
    {
        // Given
        var obj = new SimpleObject
        {
            Name = null,
            Age = 25,
            IsActive = true
        };
        
        // When
        var result = TamlSerializer.Serialize(obj);
        
        // Then
        Assert.Contains("Name\tnull", result);
    }
    
    #endregion
    
    #region Collection Tests
    
    [Fact]
    public void GivenListOfStrings_WhenSerializing_ThenReturnsIndentedItems()
    {
        // Given
        var obj = new ListContainer
        {
            Items = new List<string> { "first", "second", "third" }
        };
        
        // When
        var result = TamlSerializer.Serialize(obj);
        
        // Then
        Assert.Contains("Items\n", result);
        Assert.Contains("\tfirst\n", result);
        Assert.Contains("\tsecond\n", result);
        Assert.Contains("\tthird\n", result);
    }
    
    [Fact]
    public void GivenListOfIntegers_WhenSerializing_ThenReturnsIndentedNumbers()
    {
        // Given
        var obj = new NumberListContainer
        {
            Numbers = new List<int> { 1, 2, 3, 4, 5 }
        };
        
        // When
        var result = TamlSerializer.Serialize(obj);
        
        // Then
        Assert.Contains("Numbers\n", result);
        Assert.Contains("\t1\n", result);
        Assert.Contains("\t2\n", result);
        Assert.Contains("\t5\n", result);
    }
    
    [Fact]
    public void GivenArray_WhenSerializing_ThenReturnsIndentedItems()
    {
        // Given
        var obj = new string[] { "alpha", "beta", "gamma" };
        
        // When
        var result = TamlSerializer.Serialize(obj);
        
        // Then
        Assert.Contains("alpha\n", result);
        Assert.Contains("beta\n", result);
        Assert.Contains("gamma\n", result);
    }
    
    #endregion
    
    #region Nested Object Tests
    
    [Fact]
    public void GivenNestedObject_WhenSerializing_ThenReturnsProperIndentation()
    {
        // Given
        var obj = new ParentObject
        {
            Name = "Parent",
            Child = new ChildObject
            {
                Name = "Child",
                Value = 100
            }
        };
        
        // When
        var result = TamlSerializer.Serialize(obj);
        
        // Then
        Assert.Contains("Name\tParent\n", result);
        Assert.Contains("Child\n", result);
        Assert.Contains("\tName\tChild\n", result);
        Assert.Contains("\tValue\t100\n", result);
    }
    
    [Fact]
    public void GivenDeeplyNestedObject_WhenSerializing_ThenReturnsMultipleLevelsOfIndentation()
    {
        // Given
        var obj = new Level1
        {
            Name = "L1",
            Level2 = new Level2
            {
                Name = "L2",
                Level3 = new Level3
                {
                    Name = "L3",
                    Value = "deep"
                }
            }
        };
        
        // When
        var result = TamlSerializer.Serialize(obj);
        
        // Then
        Assert.Contains("Name\tL1\n", result);
        Assert.Contains("Level2\n", result);
        Assert.Contains("\tName\tL2\n", result);
        Assert.Contains("\tLevel3\n", result);
        Assert.Contains("\t\tName\tL3\n", result);
        Assert.Contains("\t\tValue\tdeep\n", result);
    }
    
    #endregion
    
    #region Complex Scenarios
    
    [Fact]
    public void GivenObjectWithListOfComplexObjects_WhenSerializing_ThenReturnsProperStructure()
    {
        // Given
        var obj = new TeamContainer
        {
            TeamName = "Development",
            Members = new List<Person>
            {
                new Person { Name = "Alice", Age = 30 },
                new Person { Name = "Bob", Age = 25 }
            }
        };
        
        // When
        var result = TamlSerializer.Serialize(obj);
        
        // Then
        Assert.Contains("TeamName\tDevelopment\n", result);
        Assert.Contains("Members\n", result);
        Assert.Contains("\tName\tAlice\n", result);
        Assert.Contains("\tAge\t30\n", result);
        Assert.Contains("\tName\tBob\n", result);
        Assert.Contains("\tAge\t25\n", result);
    }
    
    [Fact]
    public void GivenComplexConfiguration_WhenSerializing_ThenMatchesSpecExample()
    {
        // Given
        var obj = new AppConfig
        {
            Application = "MyApp",
            Version = "1.0.0",
            Server = new ServerConfig
            {
                Host = "0.0.0.0",
                Port = 8080,
                Ssl = true
            }
        };
        
        // When
        var result = TamlSerializer.Serialize(obj);
        
        // Then
        Assert.Contains("Application\tMyApp\n", result);
        Assert.Contains("Version\t1.0.0\n", result);
        Assert.Contains("Server\n", result);
        Assert.Contains("\tHost\t0.0.0.0\n", result);
        Assert.Contains("\tPort\t8080\n", result);
        Assert.Contains("\tSsl\ttrue\n", result);
    }
    
    #endregion
    
    #region Stream Tests
    
    [Fact]
    public void GivenObject_WhenSerializingToStream_ThenStreamContainsCorrectData()
    {
        // Given
        var obj = new SimpleObject { Name = "Test", Age = 30, IsActive = true };
        using var stream = new MemoryStream();
        
        // When
        TamlSerializer.Serialize(obj, stream);
        
        // Then
        stream.Position = 0;
        using var reader = new StreamReader(stream);
        var result = reader.ReadToEnd();
        
        Assert.Contains("Name\tTest", result);
        Assert.Contains("Age\t30", result);
        Assert.Contains("IsActive\ttrue", result);
    }
    
    [Fact]
    public void GivenObject_WhenSerializingToStreamMethod_ThenReturnsReadableStream()
    {
        // Given
        var obj = new SimpleObject { Name = "StreamTest", Age = 42, IsActive = false };
        
        // When
        using var stream = TamlSerializer.SerializeToStream(obj);
        
        // Then
        using var reader = new StreamReader(stream);
        var result = reader.ReadToEnd();
        
        Assert.Contains("Name\tStreamTest", result);
        Assert.Contains("Age\t42", result);
        Assert.Contains("IsActive\tfalse", result);
    }
    
    [Fact]
    public void GivenStreamSerialization_WhenReadingPosition_ThenStreamIsAtBeginning()
    {
        // Given
        var obj = new { Value = "test" };
        
        // When
        using var stream = TamlSerializer.SerializeToStream(obj);
        
        // Then
        Assert.Equal(0, stream.Position);
    }
    
    #endregion
    
    #region Test Helper Classes
    
    private class SimpleObject
    {
        public string? Name { get; set; }
        public int Age { get; set; }
        public bool IsActive { get; set; }
    }
    
    private class ListContainer
    {
        public List<string>? Items { get; set; }
    }
    
    private class NumberListContainer
    {
        public List<int>? Numbers { get; set; }
    }
    
    private class ParentObject
    {
        public string? Name { get; set; }
        public ChildObject? Child { get; set; }
    }
    
    private class ChildObject
    {
        public string? Name { get; set; }
        public int Value { get; set; }
    }
    
    private class Level1
    {
        public string? Name { get; set; }
        public Level2? Level2 { get; set; }
    }
    
    private class Level2
    {
        public string? Name { get; set; }
        public Level3? Level3 { get; set; }
    }
    
    private class Level3
    {
        public string? Name { get; set; }
        public string? Value { get; set; }
    }
    
    private class Person
    {
        public string? Name { get; set; }
        public int Age { get; set; }
    }
    
    private class TeamContainer
    {
        public string? TeamName { get; set; }
        public List<Person>? Members { get; set; }
    }
    
    private class AppConfig
    {
        public string? Application { get; set; }
        public string? Version { get; set; }
        public ServerConfig? Server { get; set; }
    }
    
    private class ServerConfig
    {
        public string? Host { get; set; }
        public int Port { get; set; }
        public bool Ssl { get; set; }
    }
    
    private class AlignedObject
    {
        public string? Short { get; set; }
        public string? Very_long_key { get; set; }
        public string? Medium { get; set; }
    }
    
    #endregion
}
