using Microsoft.Extensions.Configuration;

namespace TAML.Configuration;

public static class ConfigurationExtensions
{

	public static ConfigurationBuilder AddTamlConfiguration(this ConfigurationBuilder builder, string path)
	{
		return builder.Add(new TamlConfigurationSource(path));
	}

}

internal class TamlConfigurationSource(string path) : IConfigurationSource
{

	public IConfigurationProvider Build(IConfigurationBuilder builder)
	{
		throw new NotImplementedException();
	}

}