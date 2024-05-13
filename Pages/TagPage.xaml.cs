using TodoApp.Models;
using TodoApp.Services;

namespace TodoApp.Pages;

public partial class TagPage : ContentPage
{
    private readonly TagService _tagService;
    private int _editTagId;

    public TagPage(TagService tagService)
    {
        InitializeComponent();

        _tagService = tagService;

        Task.Run( () => listView.ItemsSource = _tagService.GetAll());
    }

    private async void saveButton_Clicked(object sender, EventArgs e)
    {
        if (_editTagId == 0)
        {
            // Add
            await _tagService.AddTag(new Tag
            {
                Name = nameField.Text,

            });
        }

        else
        {
            // Edit

            await _tagService.UpdateTag(new Tag
            {
                Id = _editTagId,
                Name = nameField.Text,
            });

            _editTagId = 0;
        }

        nameField.Text = string.Empty;

        listView.ItemsSource = _tagService.GetAll();
    }

    private async void listView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        var tag = (Tag)e.Item;
        var action = await DisplayActionSheet("Ýþlem", "Ýptal", null, "Güncelle", "Sil");

        switch (action)
        {
            case "Güncelle":

                _editTagId = tag.Id;
                nameField.Text = tag.Name;
                break;

            case "Sil":

                await _tagService.DeleteTag(tag);
                listView.ItemsSource = _tagService.GetAll();

                break;

        }
    }

 
}