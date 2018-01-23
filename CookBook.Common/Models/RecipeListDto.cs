using System;
using CookBook.Common.Enums;

namespace CookBook.Common.Models
{
    public class RecipeListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public FoodType Type { get; set; }
        public TimeSpan Duration { get; set; }

        public override string ToString()
        {
            return
                $"{nameof(this.Id)}: {this.Id}, {nameof(this.Name)}: {this.Name}, {nameof(this.Type)}: {this.Type}, {nameof(this.Duration)}: {this.Duration}";
        }
    }
}