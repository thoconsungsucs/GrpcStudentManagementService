using BlazorClient.Models;
using Microsoft.AspNetCore.Components;
using Shared;

namespace BlazorClient.Components.Pages
{
    public partial class StudentEditAdd
    {
        [Parameter]
        public StudentShared _student { get; set; }
        [Parameter]
        public List<SelectionItem> _classes { get; set; }
        List<GenderSelection> _genders = GenderList.Genders;

    }
}
