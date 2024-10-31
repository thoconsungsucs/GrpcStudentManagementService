using AntDesign;
using AntDesign.Charts;
using Microsoft.AspNetCore.Components;
using Shared;

namespace BlazorClient.Components.Pages
{
    public partial class GenderChart : ComponentBase
    {
        [Inject]
        private IStudentService StudentService { get; set; }
        [Inject]
        private IMessageService _message { get; set; }
        [Inject]
        private IClassService ClassService { get; set; }

        IChartComponent barChart;
        private List<BarChartItem>? _genderData;
        List<SelectionItem> _classes = new List<SelectionItem>();
        private RequestId _requestId = new RequestId();
        protected override async Task OnInitializedAsync()
        {
            var genderReply = await StudentService.GetGenderCountAsync(new RequestId { Value = 0 });
            if (genderReply.IsSuccess)
            {
                _genderData = genderReply.Value;
            }
            else
            {
                await _message.Error(genderReply.Error);
            }

            var classReply = await ClassService.GetClassSelectionAsync();
            if (classReply.IsSuccess)
            {
                _classes = classReply.Value;
            }
            else
            {
                await _message.Error(classReply.Error);
            }

        }

        private async void SearchByClassId(SelectionItem? item)
        {
            var reply = await StudentService.GetGenderCountAsync(item == null ? new RequestId() : new RequestId { Value = item.Id });
            if (reply.IsSuccess)
            {
                _genderData = reply.Value;
                await barChart.ChangeData(_genderData);
            }
            else
            {
                await _message.Error(reply.Error);
            }
        }

        readonly BarConfig config1 = new BarConfig
        {
            Title = new AntDesign.Charts.Title
            {
                Visible = true,
                Text = "Gender Chart"
            },
            XField = "value",
            YField = "label",
            IsGroup = true,
            SeriesField = "type",
            Color = new[] { "#1383ab", "#c52125" },
            Label = new BarViewConfigLabel()
        };
    }
}
