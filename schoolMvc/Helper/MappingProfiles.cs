using AutoMapper;
using schoolMvc.DAL.Models;
using schoolMvc.PL.ViewModel.Task;

namespace schoolMvc.PL.Helper
{
    public class MappingProfiles : Profile
    {


        public MappingProfiles()
        {

            CreateMap<SchoolTask, TaskShow>().ForMember(B => B.TeacherName, BD => BD.MapFrom(S => S.Teacher.UserName))
                                             .ForMember(B => B.StudentName, BD => BD.MapFrom(S => S.Student.UserName))
                                             .ForMember(pdto => pdto.Status, opt => opt.MapFrom(p => p.status.ToString()));




            CreateMap<SchoolTask, StudentTaskVM>().ForMember(B => B.TeacherName, BD => BD.MapFrom(S => S.Teacher.UserName))
                                                  .ForMember(pdto => pdto.Status, opt => opt.MapFrom(p => p.status.ToString()));



            CreateMap<SchoolTask, TeacherTaskVM>().ForMember(B => B.StudentName, BD => BD.MapFrom(S => S.Student.UserName))
                                                .ForMember(pdto => pdto.Status, opt => opt.MapFrom(p => p.status.ToString()));

            CreateMap<SchoolTask, TaskVM>().ReverseMap();
        }

    }
}
