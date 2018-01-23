using CookBook.Common.Models;
using Prism.Events;

namespace CookBook.App.Infrastructure.Events
{
    public class DeleteRecipeEvent : PubSubEvent<RecipeDetailDto> { }
}