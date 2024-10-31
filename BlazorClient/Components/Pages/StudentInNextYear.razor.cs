using AntDesign.Charts;
using Microsoft.AspNetCore.Components;
using Shared;

namespace BlazorClient.Components.Pages
{
    public partial class StudentInNextYear
    {
        [Inject]
        private IStudentService StudentService { get; set; }

        List<PieChartItem> _byLevel = new List<PieChartItem>();
        List<PieChartItem> _byGrade = new List<PieChartItem>();
        IChartComponent pieLevelChart = new Pie();
        IChartComponent pieGradeChart = new Pie();

        protected override async Task OnInitializedAsync()
        {
            var levelReply = await StudentService.CategoizeNextYearStudentByLevelAsync();
            if (levelReply.IsSuccess)
            {
                _byLevel = levelReply.Value;
                pieLevelChart.ChangeData(_byLevel);
            }

            var gradeReply = await StudentService.CategoizeNextYearStudentByGradeAsync();
            if (gradeReply.IsSuccess)
            {
                _byGrade = gradeReply.Value;
                pieGradeChart.ChangeData(_byGrade);
            }
        }

        readonly PieConfig config1 = new PieConfig
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
