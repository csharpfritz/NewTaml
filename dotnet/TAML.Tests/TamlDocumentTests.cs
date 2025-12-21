using TAML.Core;

namespace TAML.Tests;

public class TamlDocumentTests
{
	#region Constructor Tests

	[Fact]
	public void GivenEmptyConstructor_WhenCreatingDocument_ThenDataIsEmptyDictionary()
	{
		// Given & When
		var doc = new TamlDocument();

		// Then
		Assert.NotNull(doc.Data);
		Assert.Empty(doc.Data);
	}

	[Fact]
	public void GivenDictionaryConstructor_WhenCreatingDocument_ThenDataIsSet()
	{
		// Given
		var data = new Dictionary<string, object?>
		{
			["key1"] = "value1",
			["key2"] = 42
		};

		// When
		var doc = new TamlDocument(data);

		// Then
		Assert.Equal(2, doc.Data.Count);
		Assert.Equal("value1", doc.Data["key1"]);
		Assert.Equal(42, doc.Data["key2"]);
	}

	[Fact]
	public void GivenNullDictionary_WhenCreatingDocument_ThenDataIsEmptyDictionary()
	{
		// Given
		Dictionary<string, object?>? data = null;

		// When
		var doc = new TamlDocument(data!);

		// Then
		Assert.NotNull(doc.Data);
		Assert.Empty(doc.Data);
	}

	#endregion

	#region Indexer Tests

	[Fact]
	public void GivenExistingKey_WhenAccessingViaIndexer_ThenReturnsValue()
	{
		// Given
		var doc = new TamlDocument();
		doc.Data["test"] = "value";

		// When
		var result = doc["test"];

		// Then
		Assert.Equal("value", result);
	}

	[Fact]
	public void GivenNonExistingKey_WhenAccessingViaIndexer_ThenReturnsNull()
	{
		// Given
		var doc = new TamlDocument();

		// When
		var result = doc["nonexistent"];

		// Then
		Assert.Null(result);
	}

	[Fact]
	public void GivenIndexerSet_WhenSettingValue_ThenValueIsStored()
	{
		// Given
		var doc = new TamlDocument();

		// When
		doc["key"] = "value";

		// Then
		Assert.Equal("value", doc.Data["key"]);
	}

	[Fact]
	public void GivenIndexerSet_WhenUpdatingExistingValue_ThenValueIsUpdated()
	{
		// Given
		var doc = new TamlDocument();
		doc["key"] = "oldValue";

		// When
		doc["key"] = "newValue";

		// Then
		Assert.Equal("newValue", doc.Data["key"]);
	}

	#endregion

	#region TryGetValue Tests

	[Fact]
	public void GivenExistingKey_WhenTryGetValue_ThenReturnsTrueAndValue()
	{
		// Given
		var doc = new TamlDocument();
		doc.Data["key"] = "value";

		// When
		var result = doc.TryGetValue("key", out var value);

		// Then
		Assert.True(result);
		Assert.Equal("value", value);
	}

	[Fact]
	public void GivenNonExistingKey_WhenTryGetValue_ThenReturnsFalse()
	{
		// Given
		var doc = new TamlDocument();

		// When
		var result = doc.TryGetValue("nonexistent", out var value);

		// Then
		Assert.False(result);
		Assert.Null(value);
	}

	#endregion

	#region GetValue<T> Tests

	[Fact]
	public void GivenStringValue_WhenGetValueAsString_ThenReturnsValue()
	{
		// Given
		var doc = new TamlDocument();
		doc.Data["key"] = "value";

		// When
		var result = doc.GetValue<string>("key");

		// Then
		Assert.Equal("value", result);
	}

	[Fact]
	public void GivenIntValue_WhenGetValueAsInt_ThenReturnsValue()
	{
		// Given
		var doc = new TamlDocument();
		doc.Data["key"] = 42;

		// When
		var result = doc.GetValue<int>("key");

		// Then
		Assert.Equal(42, result);
	}

	[Fact]
	public void GivenIntValue_WhenGetValueAsString_ThenConvertsToString()
	{
		// Given
		var doc = new TamlDocument();
		doc.Data["key"] = 42;

		// When
		var result = doc.GetValue<string>("key");

		// Then
		Assert.Equal("42", result);
	}

	[Fact]
	public void GivenStringNumber_WhenGetValueAsInt_ThenConvertsToInt()
	{
		// Given
		var doc = new TamlDocument();
		doc.Data["key"] = "42";

		// When
		var result = doc.GetValue<int>("key");

		// Then
		Assert.Equal(42, result);
	}

	[Fact]
	public void GivenNonExistingKey_WhenGetValue_ThenReturnsDefault()
	{
		// Given
		var doc = new TamlDocument();

		// When
		var result = doc.GetValue<string>("nonexistent");

		// Then
		Assert.Null(result);
	}

	[Fact]
	public void GivenInvalidConversion_WhenGetValue_ThenReturnsDefault()
	{
		// Given
		var doc = new TamlDocument();
		doc.Data["key"] = "not a number";

		// When
		var result = doc.GetValue<int>("key");

		// Then
		Assert.Equal(0, result);
	}

	[Fact]
	public void GivenBooleanValue_WhenGetValueAsBool_ThenReturnsValue()
	{
		// Given
		var doc = new TamlDocument();
		doc.Data["key"] = true;

		// When
		var result = doc.GetValue<bool>("key");

		// Then
		Assert.True(result);
	}

	#endregion

	#region SetValue Tests

	[Fact]
	public void GivenSetValue_WhenCallingSetValue_ThenValueIsStored()
	{
		// Given
		var doc = new TamlDocument();

		// When
		doc.SetValue("key", "value");

		// Then
		Assert.Equal("value", doc.Data["key"]);
	}

	[Fact]
	public void GivenSetValue_WhenSettingNullValue_ThenNullIsStored()
	{
		// Given
		var doc = new TamlDocument();

		// When
		doc.SetValue("key", null);

		// Then
		Assert.True(doc.Data.ContainsKey("key"));
		Assert.Null(doc.Data["key"]);
	}

	#endregion

	#region GetKeys Tests

	[Fact]
	public void GivenEmptyDocument_WhenGetKeys_ThenReturnsEmptyCollection()
	{
		// Given
		var doc = new TamlDocument();

		// When
		var keys = doc.GetKeys();

		// Then
		Assert.Empty(keys);
	}

	[Fact]
	public void GivenDocumentWithKeys_WhenGetKeys_ThenReturnsAllKeys()
	{
		// Given
		var doc = new TamlDocument();
		doc.Data["key1"] = "value1";
		doc.Data["key2"] = "value2";
		doc.Data["key3"] = "value3";

		// When
		var keys = doc.GetKeys().ToList();

		// Then
		Assert.Equal(3, keys.Count);
		Assert.Contains("key1", keys);
		Assert.Contains("key2", keys);
		Assert.Contains("key3", keys);
	}

	#endregion

	#region ContainsKey Tests

	[Fact]
	public void GivenExistingKey_WhenContainsKey_ThenReturnsTrue()
	{
		// Given
		var doc = new TamlDocument();
		doc.Data["key"] = "value";

		// When
		var result = doc.ContainsKey("key");

		// Then
		Assert.True(result);
	}

	[Fact]
	public void GivenNonExistingKey_WhenContainsKey_ThenReturnsFalse()
	{
		// Given
		var doc = new TamlDocument();

		// When
		var result = doc.ContainsKey("nonexistent");

		// Then
		Assert.False(result);
	}

	#endregion

	#region GetSection Tests

	[Fact]
	public void GivenNestedSection_WhenGetSection_ThenReturnsDocumentForSection()
	{
		// Given
		var doc = new TamlDocument();
		var nestedData = new Dictionary<string, object?>
		{
			["nested1"] = "value1",
			["nested2"] = 42
		};
		doc.Data["section"] = nestedData;

		// When
		var section = doc.GetSection("section");

		// Then
		Assert.NotNull(section);
		Assert.Equal("value1", section.Data["nested1"]);
		Assert.Equal(42, section.Data["nested2"]);
	}

	[Fact]
	public void GivenNonExistingSection_WhenGetSection_ThenReturnsNull()
	{
		// Given
		var doc = new TamlDocument();

		// When
		var section = doc.GetSection("nonexistent");

		// Then
		Assert.Null(section);
	}

	[Fact]
	public void GivenNonDictionaryValue_WhenGetSection_ThenReturnsNull()
	{
		// Given
		var doc = new TamlDocument();
		doc.Data["key"] = "string value";

		// When
		var section = doc.GetSection("key");

		// Then
		Assert.Null(section);
	}

	#endregion

	#region Flatten Tests

	[Fact]
	public void GivenFlatDocument_WhenFlatten_ThenReturnsSimpleKeys()
	{
		// Given
		var doc = new TamlDocument();
		doc.Data["key1"] = "value1";
		doc.Data["key2"] = "value2";

		// When
		var flattened = doc.Flatten();

		// Then
		Assert.Equal(2, flattened.Count);
		Assert.Equal("value1", flattened["key1"]);
		Assert.Equal("value2", flattened["key2"]);
	}

	[Fact]
	public void GivenNestedDocument_WhenFlatten_ThenReturnsColonSeparatedKeys()
	{
		// Given
		var doc = new TamlDocument();
		var nestedData = new Dictionary<string, object?>
		{
			["nested1"] = "value1",
			["nested2"] = "value2"
		};
		doc.Data["section"] = nestedData;

		// When
		var flattened = doc.Flatten();

		// Then
		Assert.Equal(2, flattened.Count);
		Assert.Equal("value1", flattened["section:nested1"]);
		Assert.Equal("value2", flattened["section:nested2"]);
	}

	[Fact]
	public void GivenDeeplyNestedDocument_WhenFlatten_ThenReturnsMultiLevelKeys()
	{
		// Given
		var doc = new TamlDocument();
		var level2 = new Dictionary<string, object?> { ["key"] = "value" };
		var level1 = new Dictionary<string, object?> { ["level2"] = level2 };
		doc.Data["level1"] = level1;

		// When
		var flattened = doc.Flatten();

		// Then
		Assert.Single(flattened);
		Assert.Equal("value", flattened["level1:level2:key"]);
	}

	[Fact]
	public void GivenArrayValue_WhenFlatten_ThenReturnsIndexedKeys()
	{
		// Given
		var doc = new TamlDocument();
		doc.Data["items"] = new List<string> { "item1", "item2", "item3" };

		// When
		var flattened = doc.Flatten();

		// Then
		Assert.Equal(3, flattened.Count);
		Assert.Equal("item1", flattened["items:0"]);
		Assert.Equal("item2", flattened["items:1"]);
		Assert.Equal("item3", flattened["items:2"]);
	}

	[Fact]
	public void GivenArrayOfDictionaries_WhenFlatten_ThenReturnsIndexedNestedKeys()
	{
		// Given
		var doc = new TamlDocument();
		var item1 = new Dictionary<string, object?> { ["name"] = "John", ["age"] = "30" };
		var item2 = new Dictionary<string, object?> { ["name"] = "Jane", ["age"] = "25" };
		doc.Data["people"] = new List<object> { item1, item2 };

		// When
		var flattened = doc.Flatten();

		// Then
		Assert.Equal(4, flattened.Count);
		Assert.Equal("John", flattened["people:0:name"]);
		Assert.Equal("30", flattened["people:0:age"]);
		Assert.Equal("Jane", flattened["people:1:name"]);
		Assert.Equal("25", flattened["people:1:age"]);
	}

	[Fact]
	public void GivenNullValue_WhenFlatten_ThenIncludesNullEntry()
	{
		// Given
		var doc = new TamlDocument();
		doc.Data["key"] = null;

		// When
		var flattened = doc.Flatten();

		// Then
		Assert.Single(flattened);
		Assert.True(flattened.ContainsKey("key"));
		Assert.Null(flattened["key"]);
	}

	[Fact]
	public void GivenPrefix_WhenFlatten_ThenIncludesPrefixInKeys()
	{
		// Given
		var doc = new TamlDocument();
		doc.Data["key"] = "value";

		// When
		var flattened = doc.Flatten("prefix");

		// Then
		Assert.Single(flattened);
		Assert.Equal("value", flattened["prefix:key"]);
	}

	[Fact]
	public void GivenEmptyDocument_WhenFlatten_ThenReturnsEmptyDictionary()
	{
		// Given
		var doc = new TamlDocument();

		// When
		var flattened = doc.Flatten();

		// Then
		Assert.Empty(flattened);
	}

	[Fact]
	public void GivenFlattenedKeys_WhenAccessing_ThenIsCaseInsensitive()
	{
		// Given
		var doc = new TamlDocument();
		doc.Data["Key"] = "value";

		// When
		var flattened = doc.Flatten();

		// Then
		Assert.Equal("value", flattened["key"]);
		Assert.Equal("value", flattened["KEY"]);
		Assert.Equal("value", flattened["Key"]);
	}

	#endregion

	#region Parse Tests

	[Fact]
	public void GivenSimpleTaml_WhenParse_ThenReturnsDocument()
	{
		// Given
		var taml = "key\tvalue\nnumber\t42";

		// When
		var doc = TamlDocument.Parse(taml);

		// Then
		Assert.NotNull(doc);
		Assert.Equal("value", doc["key"]);
		Assert.Equal(42, doc.GetValue<int>("number"));
	}

	[Fact]
	public void GivenEmptyString_WhenParse_ThenReturnsEmptyDocument()
	{
		// Given
		var taml = "";

		// When
		var doc = TamlDocument.Parse(taml);

		// Then
		Assert.NotNull(doc);
		Assert.Empty(doc.Data);
	}

	[Fact]
	public void GivenWhitespaceString_WhenParse_ThenReturnsEmptyDocument()
	{
		// Given
		var taml = "   \n\t  ";

		// When
		var doc = TamlDocument.Parse(taml);

		// Then
		Assert.NotNull(doc);
		Assert.Empty(doc.Data);
	}

	[Fact]
	public void GivenNestedTaml_WhenParse_ThenReturnsNestedDocument()
	{
		// Given
		var taml = "section\n\tkey\tvalue\n\tnumber\t42";

		// When
		var doc = TamlDocument.Parse(taml);

		// Then
		var section = doc.GetSection("section");
		Assert.NotNull(section);
		Assert.Equal("value", section["key"]);
		Assert.Equal(42, section.GetValue<int>("number"));
	}

	#endregion

	#region ToString Tests

	[Fact]
	public void GivenDocument_WhenToString_ThenReturnsTamlString()
	{
		// Given
		var doc = new TamlDocument();
		doc.Data["key"] = "value";
		doc.Data["number"] = 42;

		// When
		var result = doc.ToString();

		// Then
		Assert.NotNull(result);
		Assert.Contains("key\t", result);
		Assert.Contains("value", result);
	}

	[Fact]
	public void GivenEmptyDocument_WhenToString_ThenReturnsEmptyOrMinimalString()
	{
		// Given
		var doc = new TamlDocument();

		// When
		var result = doc.ToString();

		// Then
		Assert.NotNull(result);
	}

	#endregion

	#region File Operations Tests

	[Fact]
	public void GivenValidFile_WhenLoadFromFile_ThenReturnsDocument()
	{
		// Given
		var tempFile = Path.GetTempFileName();
		File.WriteAllText(tempFile, "key\tvalue\nnumber\t42");

		try
		{
			// When
			var doc = TamlDocument.LoadFromFile(tempFile);

			// Then
			Assert.NotNull(doc);
			Assert.Equal("value", doc["key"]);
			Assert.Equal(42, doc.GetValue<int>("number"));
		}
		finally
		{
			File.Delete(tempFile);
		}
	}

	[Fact]
	public void GivenNonExistentFile_WhenLoadFromFile_ThenThrowsFileNotFoundException()
	{
		// Given
		var nonExistentFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

		// When & Then
		Assert.Throws<FileNotFoundException>(() => TamlDocument.LoadFromFile(nonExistentFile));
	}

	[Fact]
	public async Task GivenValidFile_WhenLoadFromFileAsync_ThenReturnsDocument()
	{
		// Given
		var tempFile = Path.GetTempFileName();
		await File.WriteAllTextAsync(tempFile, "key\tvalue\nnumber\t42");

		try
		{
			// When
			var doc = await TamlDocument.LoadFromFileAsync(tempFile);

			// Then
			Assert.NotNull(doc);
			Assert.Equal("value", doc["key"]);
			Assert.Equal(42, doc.GetValue<int>("number"));
		}
		finally
		{
			File.Delete(tempFile);
		}
	}

	[Fact]
	public async Task GivenNonExistentFile_WhenLoadFromFileAsync_ThenThrowsFileNotFoundException()
	{
		// Given
		var nonExistentFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

		// When & Then
		await Assert.ThrowsAsync<FileNotFoundException>(() => TamlDocument.LoadFromFileAsync(nonExistentFile));
	}

	[Fact]
	public void GivenDocument_WhenSaveToFile_ThenFileContainsTamlContent()
	{
		// Given
		var doc = new TamlDocument();
		doc.Data["key"] = "value";
		doc.Data["number"] = 42;
		var tempFile = Path.GetTempFileName();

		try
		{
			// When
			doc.SaveToFile(tempFile);

			// Then
			Assert.True(File.Exists(tempFile));
			var content = File.ReadAllText(tempFile);
			Assert.Contains("key\t", content);
			Assert.Contains("value", content);
		}
		finally
		{
			File.Delete(tempFile);
		}
	}

	[Fact]
	public async Task GivenDocument_WhenSaveToFileAsync_ThenFileContainsTamlContent()
	{
		// Given
		var doc = new TamlDocument();
		doc.Data["key"] = "value";
		doc.Data["number"] = 42;
		var tempFile = Path.GetTempFileName();

		try
		{
			// When
			await doc.SaveToFileAsync(tempFile);

			// Then
			Assert.True(File.Exists(tempFile));
			var content = await File.ReadAllTextAsync(tempFile);
			Assert.Contains("key\t", content);
			Assert.Contains("value", content);
		}
		finally
		{
			File.Delete(tempFile);
		}
	}

	[Fact]
	public void GivenSavedFile_WhenLoadingAgain_ThenDataMatches()
	{
		// Given
		var originalDoc = new TamlDocument();
		originalDoc.Data["key"] = "value";
		originalDoc.Data["number"] = 42;
		var tempFile = Path.GetTempFileName();

		try
		{
			// When
			originalDoc.SaveToFile(tempFile);
			var loadedDoc = TamlDocument.LoadFromFile(tempFile);

			// Then
			Assert.Equal("value", loadedDoc["key"]);
			Assert.Equal(42, loadedDoc.GetValue<int>("number"));
		}
		finally
		{
			File.Delete(tempFile);
		}
	}

	#endregion
}
