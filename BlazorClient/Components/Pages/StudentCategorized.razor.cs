using AntDesign;
using AntDesign.Charts;
using Microsoft.AspNetCore.Components;
using Shared;
using Title = AntDesign.Charts.Title;

namespace BlazorClient.Components.Pages
{
    public partial class StudentCategorized
    {
        [Inject]
        private IStudentService StudentService { get; set; }
        [Inject]
        private IMessageService _message { get; set; }
        [Inject]
        private ILevelService LevelService { get; set; }
        [Inject]
        private IGradeService GradeService { get; set; }

        IChartComponent pieChart;
        StudentCategorizeOption _option = new StudentCategorizeOption { LevelName = string.Empty, ByClass = false, ByGrade = false, GradeId = 0 };
        List<string> _levels = new List<string>();
        List<SelectionItem> _grades = new List<SelectionItem>();
        SelectionItem defaultSelectedGrade = new SelectionItem { Id = 0, Name = "All" };
        protected override async Task OnInitializedAsync()
        {
            pieChart = new Pie();
            await LoadData();
            var levelReply = await LevelService.GetAllLevelsAsync();
            if (levelReply.IsSuccess)
            {
                _levels = levelReply.Value.Select(l => l.LevelName).ToList();
            }
        }
        async Task CheckChangedByGrade()
        {
            // Reset option
            _option.ByClass = false;
            _option.GradeId = 0;
            defaultSelectedGrade = new SelectionItem { Id = 0, Name = "All" };
            await ResetChart();
        }

        async Task CheckChangedByLevel()
        {
            // Reset option
            _option.ByGrade = false;
            _option.GradeId = 0;
            defaultSelectedGrade = new SelectionItem { Id = 0, Name = "All" };

            await ResetChart();
        }
        async Task ResetChart()
        {
            var studentReply = await StudentService.CategorizeStudent(_option);
            if (studentReply.IsSuccess)
            {
                data2 = studentReply.Value.ToArray();
                pieChart.ChangeData(data2);
            }
        }

        async Task CategorizeByLevelName(string? levelName)
        {
            if (levelName != null)
            {
                _option.LevelName = levelName;
                var gradeReply = await GradeService.GetGradeSelectionByLevelNameAsync(levelName);
                if (gradeReply.IsSuccess)
                {
                    _grades = gradeReply.Value;
                }
            }
            else
            {
                _option.LevelName = string.Empty;
            }
            // Reset grade
            _option.GradeId = 0;
            await ResetChart();
        }

        async void CategorizeByGradeId(SelectionItem item)
        {
            if (item != null)
            {
                _option.GradeId = item.Id;
                _option.ByGrade = false;
            }
            else
            {
                _option.GradeId = 0;
                _option.ByGrade = true;
            }
            await ResetChart();
        }

        async Task LoadData()
        {
            try
            {
                var studentReply = await StudentService.CategorizeStudent(_option);
                if (studentReply.IsSuccess)
                {
                    data2 = studentReply.Value.ToArray();
                }
                pieChart.ChangeData(data2);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        object[] data2 =
        {
        };

        readonly PieConfig config2 = new PieConfig
        {
            ForceFit = true,
            Title = new Title
            {
                Visible = true,
                Text = "Donut Chart"
            },
            Description = new Description
            {
                Visible = true,
                Text = "The ring chart indicator card can replace the tooltip and display the detailed information of each category in the hollowed-out part of the ring chart."
            },
            AppendPadding = 10,
            InnerRadius = 0.6,
            Radius = 0.8,
            Padding = "auto",
            AngleField = "value",
            ColorField = "type",

        };
    }
}
