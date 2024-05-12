using System;
using System.ComponentModel.DataAnnotations;

namespace BussinessObject
{
    internal class DiscountValidAttribute : ValidationAttribute
    {
        public DiscountValidAttribute()
        {
            ErrorMessage = "Discount must be from 1 to 100";
        }
        public override bool IsValid(object value)
        {
            if (value == null) return false;
            double discount = double.Parse(value.ToString());
            if (discount > 100 || discount < 0)
            {
                return false;
            }
            else return true;
        }
    }
}