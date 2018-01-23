using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AutoMapper;
using CookBook.App.Infrastructure.Events;
using CookBook.Common.Interfaces;
using CookBook.Common.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace CookBook.App.Recipes.ViewModels
{
    public class RecipeListViewModel : BindableBase
    {
        private ObservableCollection<RecipeListDto> _recipes;
        private bool _isLoading = true;

        public string Title { get; } = "Recipes";

        public RecipeListViewModel(ICookBookRepository cookBookRepository, IEventAggregator eventAggregator)
        {
            this.CookBookRepository = cookBookRepository;
            this.EventAggregator = eventAggregator;

            this.EventAggregator.GetEvent<UpdateRecipeEvent>().Subscribe(this.UpdateRecipe);
            this.EventAggregator.GetEvent<DeleteRecipeEvent>().Subscribe(this.RemoveRecipe);
            this.Recipes = new ObservableCollection<RecipeListDto>();
            this.OnLoad().ConfigureAwait(false); 
        }

        private void RemoveRecipe(RecipeDetailDto recipeDetailDto)
        {
            this.RemoveRecipeById(recipeDetailDto.Id);
        }

        private void UpdateRecipe(RecipeDetailDto recipeDetailDto)
        {
            this.RemoveRecipeById(recipeDetailDto.Id);
            this.Recipes.Add(new RecipeListDto()
            {
                Id = recipeDetailDto.Id,
                Name = recipeDetailDto.Name,
                Duration = recipeDetailDto.Duration,
                Type = recipeDetailDto.Type,
            });
        }

        private void RemoveRecipeById(Guid id)
        {
            var recipeListDto = this.Recipes.FirstOrDefault(i => i.Id == id);
            this.Recipes.Remove(recipeListDto);
        }

        private async Task OnLoad()
        {
            await Task.Run(async () =>
            {
                this.IsLoading = true;
                this.Recipes = new ObservableCollection<RecipeListDto>(await this.CookBookRepository.GetAllRecipesAsync());
                this.IsLoading = false;
            });
        }

        public ObservableCollection<RecipeListDto> Recipes
        {
            get => this._recipes;
            set => this.SetProperty(ref this._recipes, value);
        }

        private ICookBookRepository CookBookRepository { get; }
        private IEventAggregator EventAggregator { get; }

        public DelegateCommand<RecipeListDto> SelectRecipeCommand => new DelegateCommand<RecipeListDto>(SelectRecipe);
        

        public bool IsLoading
        {
            get => this._isLoading;
            set => this.SetProperty(ref this._isLoading, value);
        }
        
        private void SelectRecipe(RecipeListDto recipeListModel)
        {
            this.EventAggregator.GetEvent<SelectedRecipeEvent>().Publish(recipeListModel as RecipeListDto);
        }
    }
}
