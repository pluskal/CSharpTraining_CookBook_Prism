using System;
using System.Collections.Generic;

namespace CookBook.BL.Models
{
    public class RecipeDetailModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public FoodType Type { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
        public ICollection<IngredienceModel> Ingredients { get; set; } = new List<IngredienceModel>();

        protected bool Equals(RecipeDetailModel other)
        {
            var members = this.Id.Equals(other.Id) && string.Equals(this.Name, other.Name) && this.Type == other.Type &&
                          string.Equals(this.Description, other.Description) && this.Duration.Equals(other.Duration);
            if (!members) return false;
            if (this.Ingredients.Count != other.Ingredients.Count) return false;
            foreach (var ingredientModel in this.Ingredients)
                if (!other.Ingredients.Contains(ingredientModel)) return false;
            return true;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return this.Equals((RecipeDetailModel) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = this.Id.GetHashCode();
                hashCode = (hashCode * 397) ^ (this.Name != null ? this.Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (int) this.Type;
                hashCode = (hashCode * 397) ^ (this.Description != null ? this.Description.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ this.Duration.GetHashCode();
                hashCode = (hashCode * 397) ^ (this.Ingredients != null ? this.Ingredients.GetHashCode() : 0);
                return hashCode;
            }
        }

        public override string ToString()
        {
            return
                $"{nameof(this.Id)}: {this.Id}, {nameof(this.Name)}: {this.Name}, {nameof(this.Type)}: {this.Type}, {nameof(this.Description)}: {this.Description}, {nameof(this.Duration)}: {this.Duration}, {nameof(this.Ingredients)}: {this.Ingredients}";
        }
    }
}