using CookBook.Common.Models;
using Prism.Events;

namespace CookBook.App.Infrastructure.Events
{
    public class SelectedRecipeEvent : PubSubEvent<RecipeListDto>{}
}