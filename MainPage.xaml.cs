using TodoApp.Models;
using TodoApp.Services;
using TodoApp.Pages;
//using TodoApp.ViewModels;

namespace TodoApp
{
    public partial class MainPage : ContentPage
    {
        private readonly TodoService _todoService;
        private readonly StatusService _statusService;
        private readonly TagService _tagService;
        private readonly TodoTagService _todoTagService;
        private int _editTodoId;

        public MainPage(TodoService todoService, StatusService statusService, TagService tagService, TodoTagService todoTagService)
        {
            InitializeComponent();

            _todoService = todoService;
            _statusService = statusService;
            _tagService = tagService;
            _todoTagService = todoTagService;

            Task.Run(async () => listView.ItemsSource = await _todoService.GetAll());
            Task.Run(async () => statusField.ItemsSource = await _statusService.GetAll());
            tagField.ItemsSource = _tagService.GetAll();


        }

        private async void saveButton_Clicked(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(nameField.Text) || string.IsNullOrWhiteSpace(descriptionField.Text) || statusField.SelectedIndex < 0 || tagField.SelectedItems.Count == 0)
            {
                await DisplayAlert("Uyarı", "Lütfen boşlukları doldurunuz.", "Tamam");
            }

            else
            {

                if (_editTodoId == 0)
                {
                    // Add

                    var selectedStatus = (Status)statusField.SelectedItem;
                    var selectedTags = tagField.SelectedItems.Cast<Tag>().Select(tag => tag.Id).ToList();
                    var selectedDate = dateField.Date;

                    await _todoService.AddTodoWithTagsAsync(new Todo
                    {
                        Name = nameField.Text,
                        Description = descriptionField.Text,
                        StatusId = selectedStatus.Id,
                        DateCreated = selectedDate

                    }, selectedTags);
                }

                else
                {
                    // Edit
                    var selectedStatus = (Status)statusField.SelectedItem;
                    var selectedTags = tagField.SelectedItems.Cast<Tag>().Select(tag => tag.Id).ToList();

                    await _todoService.UpdateTodo(new Todo
                    {
                        Id = _editTodoId,
                        Name = nameField.Text,
                        Description = descriptionField.Text,
                        StatusId = selectedStatus.Id,


                    }, selectedTags);

                    _editTodoId = 0;
                }

                listView.ItemsSource = await _todoService.GetAll();
                nameField.Text = string.Empty;
                descriptionField.Text = string.Empty;
                statusField.ItemsSource = await _statusService.GetAll();
                tagField.SelectedItems = null;

            }

        }

        private async void listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var todo = (Todo)e.Item;
            var action = await DisplayActionSheet("İşlem", "İptal", null, "Güncelle", "Sil");


            switch (action)
            {
                case "Güncelle":

                    _editTodoId = todo.Id;
                    nameField.Text = todo.Name;
                    descriptionField.Text = todo.Description;
                    statusField.SelectedIndex = todo.StatusId;
                    break;

                case "Sil":

                    await _todoService.DeleteTodo(todo);
                    listView.ItemsSource = await _todoService.GetAll();
                    break;

            }
        }

        private async void statusPage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new StatusPage(_statusService));
        }

        private async void tagPage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new TagPage(_tagService));
        }

    }

}
