using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace WebApplication3.ViewModels
{
    public class Register
    {

		[Required(ErrorMessage = "First Name is required")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "Last Name is required")]
		public string LastName { get; set; }

		[Required(ErrorMessage = "Credit Card No is required")]
		[RegularExpression(@"^\d{16}$", ErrorMessage = "Credit Card No must be 16 digits")]
		public string CreditCardNo { get; set; }

		[Required(ErrorMessage = "Mobile No is required")]
		[RegularExpression(@"^\d{8}$", ErrorMessage = "Mobile No must be 8 digits")]
		public string MobileNo { get; set; }

		[Required(ErrorMessage = "Billing Address is required")]
		public string BillingAddress { get; set; }

		[Required(ErrorMessage = "Shipping Address is required")]
		[RegularExpression(@"^[a-zA-Z0-9\s\.,#-]+$", ErrorMessage = "Shipping Address can contain only alphanumeric characters, spaces, and special characters ,.#-")]
		public string ShippingAddress { get; set; }

		[Required(ErrorMessage = "Email Address is required")]
		[DataType(DataType.EmailAddress)]
		[EmailAddress(ErrorMessage = "Invalid Email Address")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Password is required")]
		[DataType(DataType.Password)]
		[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{12,}$",ErrorMessage = "Password must be at least 12 characters long and include lowercase, uppercase, numeric, and special characters.")]
		public string Password { get; set; }

		[Required(ErrorMessage = "Confirm Password is required")]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }

		[Required(ErrorMessage = "Photo is required")]
		[DataType(DataType.Upload)]
		[AllowedExtensions(new string[] { ".jpg" }, ErrorMessage = "Only .jpg files allowed")]
		public IFormFile Photo { get; set; }
	}
	public class AllowedExtensionsAttribute : ValidationAttribute
	{
		private readonly string[] _extensions;

		public AllowedExtensionsAttribute(string[] extensions)
		{
			_extensions = extensions;
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (value == null)
				return ValidationResult.Success;

			IFormFile file = value as IFormFile;
			string extension = System.IO.Path.GetExtension(file.FileName);

			if (file != null)
			{
				if (!_extensions.Contains(extension.ToLower()))
				{
					return new ValidationResult(GetErrorMessage());
				}
			}

			return ValidationResult.Success;
		}

		public string GetErrorMessage()
		{
			return $"This file extension is not allowed!";
		}
	}
}
