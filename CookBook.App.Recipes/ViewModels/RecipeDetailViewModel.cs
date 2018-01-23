using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using CookBook.App.Infrastructure.Events;
using CookBook.Common.Enums;
using CookBook.Common.Interfaces;
using CookBook.Common.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace CookBook.App.Recipes.ViewModels
{
    public class RecipeDetailViewModel : BindableBase
    {
        private RecipeDetailDto _detail;
        public ICookBookRepository CookBookRepository { get; }
        public IEventAggregator EventAggregator { get; }

        public string Title { get; } = "Recipe detail";

        public RecipeDetailViewModel(ICookBookRepository cookBookRepository, IEventAggregator eventAggregator)
        {
            this.CookBookRepository = cookBookRepository;
            this.EventAggregator = eventAggregator;

            this.EventAggregator.GetEvent<SelectedRecipeEvent>().Subscribe(this.ChangeSelectedRecipe);
            this.NewRecipeDetailCommand =
                new DelegateCommand(this.CreateNewRecipe);
            this.SaveRecipeDetailCommand =
                new DelegateCommand(this.SaveRecipe);
            this.DeleteRecipeDetailCommand =
                new DelegateCommand(this.DeleteRecipe);

            this.CreateNewRecipe();
        }

        private void DeleteRecipe()
        {
            this.CookBookRepository.RemoveRecipe(this.Detail.Id);
            this.EventAggregator.GetEvent<DeleteRecipeEvent>().Publish(this.Detail);
        }

        private void SaveRecipe()
        {
            this.CookBookRepository.InsertOrUpdateRecipe(this.Detail);
            this.EventAggregator.GetEvent<UpdateRecipeEvent>().Publish(this.Detail);
        }

        private void CreateNewRecipe()
        {
            this.Detail = new RecipeDetailDto();
        }

        private void ChangeSelectedRecipe(RecipeListDto recipeListDto)
        {
            if (recipeListDto != null)
            {
                this.SelectRecipe(recipeListDto.Id);
            }
            else
            {
                this.CreateNewRecipe();
            }
        }

        private void SelectRecipe(Guid id)
        {
            this.Detail = this.CookBookRepository.GetRecipeById(id);
        }

        public RecipeDetailDto Detail
        {
            get => this._detail;
            set => this.SetProperty(ref this._detail, value);
        }

        public IList<FoodType> FoodTypes => Enum.GetValues(typeof(FoodType)).Cast<FoodType>().ToList();

        public ICommand SaveRecipeDetailCommand { get; }
        public ICommand DeleteRecipeDetailCommand { get; }

        public ICommand NewRecipeDetailCommand { get; }
    }

}
