using System;

namespace CookBook.BL.Models
{
    public class IngredienceDetailModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        protected bool Equals(IngredienceDetailModel other)
        {
            return this.Id.Equals(other.Id) && string.Equals(this.Name, other.Name) &&
                   string.Equals(this.Description, other.Description);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return this.Equals((IngredienceDetailModel) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = this.Id.GetHashCode();
                hashCode = (hashCode * 397) ^ (this.Name != null ? this.Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Description != null ? this.Description.GetHashCode() : 0);
                return hashCode;
            }
        }

        public override string ToString()
        {
            return
                $"{nameof(this.Id)}: {this.Id}, {nameof(this.Name)}: {this.Name}, {nameof(this.Description)}: {this.Description}";
        }
    }
}