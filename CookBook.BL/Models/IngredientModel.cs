using System;
using CookBook.Common;

namespace CookBook.BL.Models
{
    public class IngredientModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public Unit Unit { get; set; }

        protected bool Equals(IngredientModel other)
        {
            return string.Equals(this.Name, other.Name) && string.Equals(this.Description, other.Description) &&
                   this.Amount.Equals(other.Amount) && this.Unit == other.Unit;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return this.Equals((IngredientModel) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = this.Name != null ? this.Name.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (this.Description != null ? this.Description.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ this.Amount.GetHashCode();
                hashCode = (hashCode * 397) ^ (int) this.Unit;
                return hashCode;
            }
        }

        public override string ToString()
        {
            return
                $"{nameof(this.Name)}: {this.Name}, {nameof(this.Description)}: {this.Description}, {nameof(this.Amount)}: {this.Amount}, {nameof(this.Unit)}: {this.Unit}";
        }
    }
}