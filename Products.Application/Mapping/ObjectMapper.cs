using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;

namespace Products.APPLICATION.Mapping;

public class ObjectMapper
{
	private static readonly ConcurrentDictionary<string, Delegate> _cachedMappers = new();

	public static TDest Map<TDest>(object source)
	{
		if (source == null)
		{
			return default;
		}

		Type sourceType = source.GetType();
		Type destType = typeof(TDest);
		string key = $"{sourceType.FullName}->{destType.FullName}";

		Func<object, TDest> mapper = (Func<object, TDest>)_cachedMappers.GetOrAdd(key, _ =>
			CreateMapper<TDest>(sourceType));

		return mapper(source);
	}

	public static IEnumerable<TDest> Map<TDest>(IEnumerable<object> source)
	{
		return source?.Select(Map<TDest>) ?? [];
	}

	private static Func<object, TDest> CreateMapper<TDest>(Type sourceType)
	{
		Type destType = typeof(TDest);
		ParameterExpression sourceParam = Expression.Parameter(typeof(object), "source");
		ParameterExpression typedSource = Expression.Variable(sourceType, "typedSource");

		Dictionary<string, PropertyInfo> sourceProps = sourceType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
			.Where(p => p.CanRead)
			.ToDictionary(p => p.Name, p => p);

		IEnumerable<PropertyInfo> destProps = destType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
			.Where(p => p.CanWrite);

		// Check if we need constructor with parameters (for required properties)
		ConstructorInfo? constructor = destType.GetConstructors()
			.OrderByDescending(c => c.GetParameters().Length)
			.FirstOrDefault();

		Expression destCreation;
		List<Expression> bindings =
			[
				Expression.Assign(typedSource, Expression.Convert(sourceParam, sourceType))
			];

		if (constructor != null && constructor.GetParameters().Length > 0)
		{
			// Constructor with parameters (handles required properties)
			ParameterInfo[] ctorParams = constructor.GetParameters();
			List<Expression> ctorArgs = [];

			foreach (ParameterInfo param in ctorParams)
			{
				string propName = char.ToUpper(param.Name[0]) + param.Name[1..];
				if (sourceProps.TryGetValue(propName, out PropertyInfo? sourceProp) &&
					param.ParameterType.IsAssignableFrom(sourceProp.PropertyType))
				{
					MemberExpression sourceValue = Expression.Property(typedSource, sourceProp);
					ctorArgs.Add(sourceValue);
				}
				else
				{
					ctorArgs.Add(Expression.Default(param.ParameterType));
				}
			}

			destCreation = Expression.New(constructor, ctorArgs);
		}
		else
		{
			// Parameterless constructor
			destCreation = Expression.New(destType);
		}

		ParameterExpression destVar = Expression.Variable(destType, "dest");
		bindings.Add(Expression.Assign(destVar, destCreation));

		// Map remaining properties
		foreach (PropertyInfo destProp in destProps)
		{
			if (sourceProps.TryGetValue(destProp.Name, out PropertyInfo? sourceProp) &&
				destProp.PropertyType.IsAssignableFrom(sourceProp.PropertyType))
			{
				MemberExpression sourceValue = Expression.Property(typedSource, sourceProp);
				MemberExpression destProperty = Expression.Property(destVar, destProp);
				BinaryExpression assignment = Expression.Assign(destProperty, sourceValue);
				bindings.Add(assignment);
			}
		}

		bindings.Add(destVar);

		BlockExpression block = Expression.Block([typedSource, destVar], bindings);
		Expression<Func<object, TDest>> lambda = Expression.Lambda<Func<object, TDest>>(block, sourceParam);

		return lambda.Compile();
	}

	public static void ClearCache()
	{
		_cachedMappers.Clear();
	}

	//-------------------------------sn add 8-4-2026----Saving-Personal-DEtails------------//
	public static void Map<TSource, TDest>(TSource source, TDest destination)
	{
		if (source == null || destination == null)
		{
			return;
		}

		IEnumerable<PropertyInfo> sourceProps = typeof(TSource)
			.GetProperties(BindingFlags.Public | BindingFlags.Instance)
			.Where(p => p.CanRead);

		IEnumerable<PropertyInfo> destProps = typeof(TDest)
			.GetProperties(BindingFlags.Public | BindingFlags.Instance)
			.Where(p => p.CanWrite);

		foreach (PropertyInfo destProp in destProps)
		{
			PropertyInfo? sourceProp = sourceProps.FirstOrDefault(p => p.Name == destProp.Name);

			if (sourceProp != null &&
				destProp.PropertyType.IsAssignableFrom(sourceProp.PropertyType))
			{
				object? value = sourceProp.GetValue(source);
				destProp.SetValue(destination, value);
			}
		}
	}
	//------------------------------//
}