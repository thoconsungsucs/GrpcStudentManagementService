namespace BlazorClient.Components.Pages
{
    using AntDesign;
    using AntDesign.TableModels;
    using BlazorClient.Models;
    using Shared;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public partial class Students
    {
        StudentShared[]? _students;
        IEnumerable<StudentShared>? _selectedRows;
        ITable table;
        bool _visible = false;
        int _pageIndex = 1;
        int _pageSize = 4;
        int _total = 0;
        List<GenderSelection> _genders = GenderList.Genders;
        List<ClassSelection> _classes = new List<ClassSelection>();
        StudentShared _student = new StudentShared();
        string _selectedValue;
        string _selectedItem;


        protected override async Task OnInitializedAsync()
        {

            var classReply = await ClassService.GetClassSelectionAsync();
            if (classReply.IsSuccess)
            {
                _classes = classReply.Value;
            }
        }

        

        private void HandleCancel()
        {
            _student = new StudentShared();
        }

        private void Edit(StudentShared studentShared)
        {
            _visible = true;
            _student = studentShared;
        }

        private async Task Delete(int id)
        {
            var reply = await StudentService.DeleteAsync(new RequestId { Value = id });
            _total = _total - 1;
            if (reply.IsSuccess)
            {
                await _message.Success("Student deleted successfully");
            }
            else
            {
                await _message.Error("Student deleted error");
            }
            table.ReloadData();
        }

        public void RemoveSelection(int id)
        {
            _selectedRows = _selectedRows.Where(x => x.StudentId != id);
        }

        async Task OnChange(QueryModel<StudentShared> queryModel)
        {
            var pagination = new PaginationRequest
            {
                PageIndex = queryModel.PageIndex,
                PageSize = queryModel.PageSize
            };
            var reply = await StudentService.GetAllPaginationAsync(pagination);
            if (reply.IsSuccess)
            {
                _students = reply.Value.List.ToArray();
                _total = reply.Value.Total;
            }
            _pageIndex = queryModel.PageIndex;
            _pageSize = queryModel.PageSize;
            _selectedRows = [];
        }

        async Task HandleOk()
        {
            if (_student.StudentId > 0)
            {
                // Update existing student
                var reply = await StudentService.UpdateAsync(_student);
                if (reply.IsSuccess)
                {
                    await _message.Success("Update student sccessfully");
                    table.ReloadData();
                }
                else
                {
                    await _message.Error("Add student sccessfully");
                }
            }
            else
            {
                // Add new student
                var reply = await StudentService.AddAsync(_student);
                if (reply.IsSuccess)
                {
                    _visible = false;
                    await _message.Success("Add student sccessfully");
                    table.ReloadData();
                } else
                {
                    await _message.Error("Add student sccessfully");
                }
            }
        }

        async Task DeleteAll()
        {
            var stringBuilder = new StringBuilder();
            foreach (var student in _selectedRows)
            {
                var res = await StudentService.DeleteAsync(new RequestId { Value = student.StudentId });
                if (!res.IsSuccess)
                {
                    stringBuilder.AppendLine($"Deleted student with id {student.StudentId} error");
                }
            }
            table.ReloadData();
            if (stringBuilder.Length > 0)
            {
                await _message.Error(stringBuilder.ToString());
            }
            else
            {
                await _message.Success("Deleted successfully");
            }
        }
    }

}
