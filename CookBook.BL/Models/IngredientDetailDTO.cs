using System;
using CookBook.Common;

namespace CookBook.BL.Models
{
    public class IngredientDetailDto
    {
        public Guid Id { get; set; }
        public Guid IngredientId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public Unit Unit { get; set; }

        protected bool Equals(IngredientDetailDto other)
        {
            return this.Id.Equals(other.Id) && this.IngredientId.Equals(other.IngredientId) &&
                   string.Equals(this.Name, other.Name) && string.Equals(this.Description, other.Description) &&
                   this.Amount.Equals(other.Amount) && this.Unit == other.Unit;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return this.Equals((IngredientDetailDto) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = this.Id.GetHashCode();
                hashCode = (hashCode * 397) ^ this.IngredientId.GetHashCode();
                hashCode = (hashCode * 397) ^ (this.Name != null ? this.Name.GetHashCode() : 0);
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