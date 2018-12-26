using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

namespace Kindly.API.Utility
{
	public static class KindlyExtensions
	{
		/// <summary>
		/// Calculates the age.
		/// </summary>
		/// 
		/// <param name="value">The value.</param>
		public static int CalculateAge(this DateTime value)
		{
			int age = DateTime.Today.Year - value.Year;

			if (value.AddYears(age) > DateTime.Today)
				age--;

			return age;
		}

		/// <summary>
		/// Spaces a string according to its camel case.
		/// </summary>
		/// 
		/// <param name="value">The value.</param>
		public static string SpacesFromCamel(this string value)
		{
			if (value.Length <= 0)
				return value;

			var result = new List<char>();
			var array = value.ToCharArray();

			foreach (char item in array)
			{
				if (char.IsUpper(item))
				{
					result.Add(' ');
				}
				result.Add(item);
			}

			return new string(result.ToArray());
		}

		/// <summary>
		/// Converts a string to lower camel case.
		/// </summary>
		/// 
		/// <param name="value">The value.</param>
		public static string ToLowerCamelCase(this string value)
		{
			string camelCase = string.Empty;

			foreach (char @char in value)
			{
				if (char.IsUpper(@char))
					camelCase += char.ToLowerInvariant(@char);
				else
					break;
			}

			return camelCase + value.Substring(camelCase.Length);
		}

		/// <summary>
		/// Converts the string to a message field representation by applying spaces from camel and lower case.
		/// </summary>
		/// 
		/// <param name="value">The value.</param>
		/// <param name="expression">The expression.</param>
		public static string InvalidFieldMessage<T, TP>(this T value, Expression<Func<T, TP>> expression)
		{
			string name = ((MemberExpression) expression.Body).Member.Name;

			return string.Format(KindlyConstants.InvalidFieldMessage, name.SpacesFromCamel().ToLower());
		}

		/// <summary>
		/// Converts the string to a message field representation by applying spaces from camel and lower case.
		/// </summary>
		/// 
		/// <param name="value">The value.</param>
		/// <param name="expression">The expression.</param>
		public static string ExistingFieldMessage<T, TP>(this T value, Expression<Func<T, TP>> expression)
		{
			string name = ((MemberExpression)expression.Body).Member.Name;

			return string.Format(KindlyConstants.ExistingFieldMessage, name.SpacesFromCamel().ToLower());
		}

		/// <summary>
		/// Adds an application error to the http response.
		/// </summary>
		/// 
		/// <param name="response">The response.</param>
		/// <param name="message">The message.</param>
		public static async Task AddApplicationError(this HttpResponse response, string message)
		{
			response.Headers.Add("Application-Error", message);
			response.Headers.Add("Access-Control-Allow-Origin", "*");
			response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");

			await response.WriteAsync(message);
		}

	}
}