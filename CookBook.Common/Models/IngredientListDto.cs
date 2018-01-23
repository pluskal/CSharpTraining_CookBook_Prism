using System;

namespace CookBook.Common.Models
{
    public class IngredientListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        protected bool Equals(IngredientListDto other)
        {
            return string.Equals(this.Name, other.Name) && string.Equals(this.Description, other.Description);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return this.Equals((IngredientListDto) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((this.Name != null ? this.Name.GetHashCode() : 0) * 397) ^
                       (this.Description != null ? this.Description.GetHashCode() : 0);
            }
        }

        public override string ToString()
        {
            return
                $"{nameof(this.Id)}: {this.Id}, {nameof(this.Name)}: {this.Name}, {nameof(this.Description)}: {this.Description}";
        }
    }
}