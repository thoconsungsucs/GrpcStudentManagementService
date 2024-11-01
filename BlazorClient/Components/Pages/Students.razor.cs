namespace BlazorClient.Components.Pages
{
    using AntDesign;
    using AntDesign.TableModels;
    using BlazorClient.Models;
    using Microsoft.AspNetCore.Components;
    using Shared;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public partial class Students
    {

        [Inject]
        public IStudentService StudentService { get; set; }
        [Inject]
        public IClassService ClassService { get; set; }

        StudentShared[]? _students;
        IEnumerable<StudentShared>? _selectedRows;
        ITable table;
        bool _visible = false;
        int _pageIndex = 1;
        int _pageSize = 4;
        int _total = 0;
        List<GenderSelection> _genders = GenderList.Genders;
        List<SelectionItem> _classes = new List<SelectionItem>();
        StudentShared _student = new StudentShared();
        StudentFilter _filter = new StudentFilter();

        protected override async Task OnInitializedAsync()
        {
            var classReply = await ClassService.GetClassSelectionAsync();
            if (classReply.IsSuccess)
            {
                _classes = classReply.Value;
            }
        }

        public void Reset()
        {
            _filter = new StudentFilter();
        }

        async Task Search()
        {
            table.ReloadData();
        }

        private void OnTimeRangeChange(DateRangeChangedEventArgs<DateTime?[]> args)
        {
            // From [0] To [1]
            _filter.DobFrom = args.Dates[0].Value;
            _filter.DobTo = args.Dates[1].Value;
        }

        private void HandleCancel()
        {
            _student = new StudentShared();
            _visible = false;
        }

        private void Edit(StudentShared studentShared)
        {
            _visible = true;
            _student = studentShared;
        }

        private async Task Delete(int id)
        {
            var reply = await StudentService.DeleteAsync(new RequestId { Value = id });
            if (reply.IsSuccess)
            {
                await _message.Success("Student deleted successfully", 1);
            }
            else
            {
                await _message.Error("Error deleting student", 1);
            }
            table.ReloadData(_pageIndex, _pageSize);
        }

        public void RemoveSelection(int id)
        {
            _selectedRows = _selectedRows?.Where(x => x.StudentId != id);
        }

        async Task OnChange(QueryModel<StudentShared> queryModel)
        {
            _filter.PageIndex = queryModel.PageIndex;
            _filter.PageSize = queryModel.PageSize;

            var reply = await StudentService.GetAllPaginationAsync(_filter);
            if (reply.IsSuccess)
            {
                _students = reply.Value.List?.ToArray();
                _total = reply.Value.Total;
            }
            _pageIndex = queryModel.PageIndex;
            _pageSize = queryModel.PageSize;

        }

        async Task HandleOk()
        {
            if (_student.StudentId > 0)
            {
                // Update existing student
                var reply = await StudentService.UpdateAsync(_student);
                if (reply.IsSuccess)
                {
                    await _message.Success("Student updated successfully", 1);
                    _visible = false;
                    _student = new StudentShared();
                }
                else
                {
                    await _message.Error("Error updating student", 1);
                }
            }
            else
            {
                // Add new student
                var reply = await StudentService.AddAsync(_student);
                if (reply.IsSuccess)
                {
                    await _message.Success("Student added successfully", 1);
                    _visible = false;
                    _student = new StudentShared();
                }
                else
                {
                    await _message.Error("Error adding student", 1);
                }
            }
            table.ReloadData(_pageIndex, _pageSize);
        }

        async Task DeleteAll()
        {
            var stringBuilder = new StringBuilder();
            foreach (var student in _selectedRows ?? Enumerable.Empty<StudentShared>())
            {
                var res = await StudentService.DeleteAsync(new RequestId { Value = student.StudentId });
                if (!res.IsSuccess)
                {
                    stringBuilder.AppendLine($"Error deleting student with ID {student.StudentId}");
                }
            }

            _selectedRows = Enumerable.Empty<StudentShared>(); // Clear selection after deleting
            table.ReloadData();

            if (stringBuilder.Length > 0)
            {
                await _message.Error(stringBuilder.ToString(), 1);
            }
            else
            {
                await _message.Success("All selected students deleted successfully", 1);
            }
        }
    }
}
