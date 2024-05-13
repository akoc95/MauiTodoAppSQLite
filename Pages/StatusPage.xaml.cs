using TodoApp.Models;
using TodoApp.Services;

namespace TodoApp.Pages;

public partial class StatusPage : ContentPage
{
    private readonly StatusService _statusService;
    private int _editStatusId;

    public StatusPage()
    {
        InitializeComponent();
    }
    public StatusPage(StatusService statusService)
	{
		InitializeComponent();
        _statusService = statusService;
        Task.Run(async () => listView.ItemsSource = await _statusService.GetAll());
    }

    private async void saveButton_Clicked(object sender, EventArgs e)
    {
        if (_editStatusId == 0)
        {
            // Add
            await _statusService.AddStatus(new Status
            {
                Name = nameField.Text,

            });
        }

        else
        {
            // Edit

            await _statusService.UpdateStatus(new Status
            {
                Id = _editStatusId,
                Name = nameField.Text,
            });

            _editStatusId = 0;
        }

        nameField.Text = string.Empty;

        listView.ItemsSource = await _statusService.GetAll();
    }

    private async void listView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        var status = (Status)e.Item;
        var action = await DisplayActionSheet("Ýþlem", "Ýptal", null, "Güncelle", "Sil");

        switch (action)
        {
            case "Güncelle":

                _editStatusId = status.Id;
                nameField.Text = status.Name;
                break;

            case "Sil":

                await _statusService.DeleteStatus(status);
                listView.ItemsSource = await _statusService.GetAll();

                break;

        }
    }

    //private async void statusPage_Clicked(object sender, EventArgs e)
    //{
    //    await Navigation.PushModalAsync(new StatusPage());
    //}
}