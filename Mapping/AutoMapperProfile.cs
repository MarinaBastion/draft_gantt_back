using AutoMapper;
using gantt_backend.Data.Models;
using gantt_backend.Data.Models.Constructor;
using gantt_backend.Data.ViewModels;
using gantt_backend.Data.ModelsDTO;
using System.Globalization;
namespace gantt_backend.Mapping
{

public class AutoMapperProfile : Profile  
{  
   public AutoMapperProfile()  
   {  
      CultureInfo currentCulture = CultureInfo.CurrentCulture;
       CreateMap<Assignment, AssignViewModel>()
      .ForMember(dest => dest.Delay, opt => opt.MapFrom(src => src.Delay))
      .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
      .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration)) 
      .ForMember(dest => dest.Mode, opt => opt.MapFrom(src => src.Mode)) 
      .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.Unit)) 
      .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value)) 
      .ForMember(dest => dest.Start_date, opt => opt.MapFrom(src => src.StartDate.HasValue ? src.StartDate.Value.ToString("dd-MM-yyyy", currentCulture): null))
      .ForMember(dest => dest.Resource_id, opt => opt.MapFrom(src => src.UserId == null ? "" : src.UserId.ToString() ));  

       CreateMap<AssignViewModel, Assignment>()
      .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration))
      .ForMember(dest => dest.Delay, opt => opt.MapFrom(src => src.Delay )) 
      .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => DateTime.Parse(src.Start_date, Thread.CurrentThread.CurrentCulture)))
      .ForMember(dest => dest.EndDate, opt => opt.Ignore())
      .ForMember(dest => dest.Mode, opt => opt.MapFrom(src => src.Mode))
      .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
      .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.Unit))
      .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value));

       CreateMap<ApplicationUser, UserViewModel>()
      .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id.ToString()))
      .ForMember(dest => dest.parent, opt => opt.MapFrom(src => src.ParentId.ToString()))
      .ForMember(dest => dest.text, opt => opt.MapFrom(src => src.UserName)) 
      .ForMember(dest => dest.unit, opt => opt.Ignore());  

       CreateMap<UserViewModel, ApplicationUser>()
      .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id))
      .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.parent))
      .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.text));  

       CreateMap<Assignment, AssignmentDto>()
      .ForMember(dest => dest.duration, opt => opt.MapFrom(src => src.Duration))
      .ForMember(dest => dest.start_date, opt => opt.MapFrom(src => src.StartDate.HasValue ? src.StartDate.Value.ToString("dd-MM-yyyy HH:mm") : null))
      .ForMember(dest => dest.end_date, opt => opt.MapFrom(src => src.EndDate.HasValue ? src.EndDate.Value.ToString("dd-MM-yyyy HH:mm") : null))
      .ForMember(dest => dest.delay, opt => opt.MapFrom(src => src.Delay))
      .ForMember(dest => dest.duration, opt => opt.MapFrom(src => src.Duration))
      .ForMember(dest => dest.resource_id, opt => opt.MapFrom(src => src.UserId))
      .ForMember(dest => dest.value, opt => opt.MapFrom(src => src.Value))
      .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id))
      .ForMember(dest => dest.mode, opt => opt.MapFrom(src => src.Mode));  

       CreateMap<AssignmentDto, Assignment>()
      .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.duration))
      .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => Convert.ToDateTime(src.start_date, 	System.Globalization.CultureInfo.GetCultureInfo("ru-RU").DateTimeFormat)))//DateTime.Parse(src.start_date, Thread.CurrentThread.CurrentCulture)))
      .ForMember(dest => dest.EndDate, opt => opt.Ignore())
      .ForMember(dest => dest.Delay, opt => opt.MapFrom(src => src.delay))
      .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.resource_id))
      .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.value))
      .ForMember(dest => dest.Mode, opt => opt.MapFrom(src => src.mode))
      .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id))
      .ForMember(dest => dest.TaskId, opt => opt.MapFrom(src => src.task_id)); 
      

      CreateMap<gantt_backend.Data.Models.Task, TaskViewModel>()
      .ForMember(dest => dest.duration, opt => opt.MapFrom(src => src.Duration))
      .ForMember(dest => dest.start_date, opt => opt.MapFrom(src => src.StartDate.ToString("dd-MM-yyyy", currentCulture)))
      .ForMember(dest => dest.planned_start, opt => opt.MapFrom(src => src.BaseStart.HasValue ? src.BaseStart.Value.ToString("dd-MM-yyyy", currentCulture): null))
      .ForMember(dest => dest.planned_end, opt => opt.MapFrom(src => src.BaseEnd.HasValue ? src.BaseEnd.Value.ToString("dd-MM-yyyy", currentCulture): null))
      .ForMember(dest => dest.text, opt => opt.MapFrom(src => src.Text))
      .ForMember(dest => dest.progress, opt => opt.MapFrom(src => src.Progress))
      .ForMember(dest => dest.type, opt => opt.MapFrom(src => src.Type))
      .ForMember(dest => dest.project_type_id, opt => opt.MapFrom(src => src.ProjectTypeId))
      .ForMember(dest => dest.parent,opt =>  opt.MapFrom(src => src.ParentId ))
      .ForMember(dest => dest.user, opt => opt.MapFrom(src =>src.Resources))
      .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id));  
      
       CreateMap<TaskViewModel,gantt_backend.Data.Models.Task>()      
      .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.duration))
      .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => Convert.ToDateTime(src.start_date, 	System.Globalization.CultureInfo.GetCultureInfo("ru-RU").DateTimeFormat)))
      .ForMember(dest => dest.BaseStart, opt => opt.MapFrom(src => Convert.ToDateTime(src.planned_start, 	System.Globalization.CultureInfo.GetCultureInfo("ru-RU").DateTimeFormat)))
      .ForMember(dest => dest.BaseEnd, opt => opt.MapFrom(src => Convert.ToDateTime(src.planned_end, 	System.Globalization.CultureInfo.GetCultureInfo("ru-RU").DateTimeFormat)))
      .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.text))
      .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.type))
      .ForMember(dest => dest.Progress, opt => opt.MapFrom(src => src.progress))
      .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.parent))
      .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id))
      .ForMember(dest => dest.EndDate, opt => opt.Ignore());

       CreateMap<Link, LinkViewModel>()
      .ForMember(dest => dest.type, opt => opt.MapFrom(src => src.Type))
      .ForMember(dest => dest.source, opt => opt.MapFrom(src => src.SourceTaskId== null ? "" : src.SourceTaskId.ToString() )) 
      .ForMember(dest => dest.target, opt => opt.MapFrom(src => src.TargetTaskId == null ? "" : src.TargetTaskId.ToString() ));  

       CreateMap<LinkViewModel, Link>()
      .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.type))
      .ForMember(dest => dest.SourceTaskId, opt => opt.MapFrom(src => src.source )) 
      .ForMember(dest => dest.TargetTaskId, opt => opt.MapFrom(src => src.target ))
      .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id));  

       CreateMap<gantt_backend.Data.Models.Task, TaskDto>()
      .ForMember(dest => dest.duration, opt => opt.MapFrom(src => src.Duration))
      .ForMember(dest => dest.start_date, opt => opt.MapFrom(src => src.StartDate.ToString("dd-MM-yyyy HH:mm")))
      .ForMember(dest => dest.end_date, opt => opt.MapFrom(src => src.EndDate.ToString("dd-MM-yyyy HH:mm")))
      .ForMember(dest => dest.planned_start, opt => opt.MapFrom(src => src.BaseStart.HasValue ? src.BaseStart.Value.ToString("dd-MM-yyyy", currentCulture): null))
      .ForMember(dest => dest.planned_end, opt => opt.MapFrom(src => src.BaseEnd.HasValue ? src.BaseEnd.Value.ToString("dd-MM-yyyy", currentCulture): null))
      .ForMember(dest => dest.text, opt => opt.MapFrom(src => src.Text))
      .ForMember(dest => dest.progress, opt => opt.MapFrom(src => src.Progress))
      .ForMember(dest => dest.parent, opt  => opt.MapFrom(src => src.ParentId))
      .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id))
      .ForMember(dest => dest.project_type_id, opt => opt.MapFrom(src => src.ProjectType!.Id))
      .ForMember(dest => dest.user, opt => opt.Ignore());  

       CreateMap<TaskDto,gantt_backend.Data.Models.Task>()
      .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.duration))
      .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => Convert.ToDateTime(src.start_date, 	System.Globalization.CultureInfo.GetCultureInfo("ru-RU").DateTimeFormat)))
      .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => Convert.ToDateTime(src.end_date, 	System.Globalization.CultureInfo.GetCultureInfo("ru-RU").DateTimeFormat)))
      .ForMember(dest => dest.BaseStart, opt => opt.MapFrom(src =>  (src.planned_start != "") ? Convert.ToDateTime(src.planned_start , 	System.Globalization.CultureInfo.GetCultureInfo("ru-RU").DateTimeFormat): (DateTime?)null))
      .ForMember(dest => dest.BaseEnd, opt => opt.MapFrom(src =>  (src.planned_end != "") ? Convert.ToDateTime(src.planned_end, 	System.Globalization.CultureInfo.GetCultureInfo("ru-RU").DateTimeFormat): (DateTime?)null))
      .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.text))
      .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.type))
      .ForMember(dest => dest.Progress, opt => opt.MapFrom(src => src.progress))
      .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.parent))
      .ForMember(dest => dest.ParentTask, opt => opt.Ignore())
      .ForMember(dest => dest.ProjectTypeId, opt => opt.MapFrom(src => src.project_type_id))
      .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id));  

       CreateMap<Link, LinkDto>()
      .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
      .ForMember(dest => dest.SourceTaskId, opt => opt.MapFrom(src => src.SourceTaskId))
      .ForMember(dest => dest.TargetTaskId, opt => opt.MapFrom(src => src.TargetTaskId))
      .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
      .ReverseMap();  

       CreateMap<Entity, EntityDto>()
      .ForMember(dest => dest.create_date, opt => opt.MapFrom(src => src.CreateDate.ToString("dd-MM-yyyy HH:mm")))
      .ForMember(dest => dest.decsription, opt => opt.MapFrom(src => src.Decsription))
      .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.Name))
      .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id));  

       CreateMap<EntityDto,Entity>()
      .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => Convert.ToDateTime(src.create_date, 	System.Globalization.CultureInfo.GetCultureInfo("ru-RU").DateTimeFormat)))
      .ForMember(dest => dest.Decsription, opt => opt.MapFrom(src => src.decsription))
      .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.name))
      .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id)); 

      CreateMap<TypeJson, TypeJsonDto>()
      .ForMember(dest => dest.directory_id, opt => opt.MapFrom(src => src.DirectoryId))
      .ForMember(dest => dest.instance_directory_id, opt => opt.MapFrom(src => src.InstanceDirectoryId))
      .ForMember(dest => dest.task_id, opt => opt.MapFrom(src => src.TaskId))
      .ForMember(dest => dest.simple_type, opt => opt.MapFrom(src => src.SimpleType));  

       CreateMap<TypeJsonDto,TypeJson>()
      .ForMember(dest => dest.DirectoryId, opt => opt.MapFrom(src => src.directory_id))
      .ForMember(dest => dest.InstanceDirectoryId, opt => opt.MapFrom(src => src.instance_directory_id))
      .ForMember(dest => dest.TaskId, opt => opt.MapFrom(src => src.task_id))
      .ForMember(dest => dest.SimpleType, opt => opt.MapFrom(src => src.simple_type)); 

      CreateMap<Field, FieldDto>()
      .ForMember(dest => dest.create_date, opt => opt.MapFrom(src => src.CreateDate.ToString("dd-MM-yyyy HH:mm")))
      .ForMember(dest => dest.decsription, opt => opt.MapFrom(src => src.Decsription))
      .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.Name))
      .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id))
      .ForMember(dest => dest.type, opt => opt.MapFrom(src => src.Type));  

       CreateMap<FieldDto,Field>()
      .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => Convert.ToDateTime(src.create_date, 	System.Globalization.CultureInfo.GetCultureInfo("ru-RU").DateTimeFormat)))
      .ForMember(dest => dest.Decsription, opt => opt.MapFrom(src => src.decsription))
      .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.name))
      .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id))
      .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.type));

      CreateMap<Instance, InstanceDto>()
      .ForMember(dest => dest.create_date, opt => opt.MapFrom(src => src.CreateDate.ToString("dd-MM-yyyy HH:mm")))
      .ForMember(dest => dest.decsription, opt => opt.MapFrom(src => src.Decsription))
      .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.Name))
      .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id))
      .ForMember(dest => dest.parent_instance_id, opt => opt.MapFrom(src => src.ParentInstance!.Id))
      .ForMember(dest => dest.entity_id, opt => opt.MapFrom(src => src.EntityId));  

       CreateMap<InstanceDto,Instance>()
      .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => Convert.ToDateTime(src.create_date, 	System.Globalization.CultureInfo.GetCultureInfo("ru-RU").DateTimeFormat)))
      .ForMember(dest => dest.Decsription, opt => opt.MapFrom(src => src.decsription))
      .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.name))
      .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id))
      .ForMember(dest => dest.ParentInstance,  opt => opt.Ignore())
      .ForMember(dest => dest.EntityId, opt => opt.MapFrom(src => src.entity_id));

      CreateMap<Value, ValueDto>()
      .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id))
      .ForMember(dest => dest.bool_data, opt => opt.MapFrom(src => src.BoolData))
      .ForMember(dest => dest.numeric_data, opt => opt.MapFrom(src => src.NumericData))
      .ForMember(dest => dest.instance_id, opt => opt.MapFrom(src => src.InstanceId))
      .ForMember(dest => dest.text_data, opt => opt.MapFrom(src => src.TextData))
      .ForMember(dest => dest.parent_value_id, opt => opt.MapFrom(src => src.ParentValue!.Id))
      .ForMember(dest => dest.value_instance_id, opt => opt.MapFrom(src => src.ValueInstanceId))
      .ForMember(dest => dest.field_id, opt => opt.MapFrom(src => src.FieldId));  

       CreateMap<ValueDto, Value>()
      .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id))
      .ForMember(dest => dest.BoolData, opt => opt.MapFrom(src => src.bool_data))
      .ForMember(dest => dest.NumericData, opt => opt.MapFrom(src => src.numeric_data))
      .ForMember(dest => dest.TextData, opt => opt.MapFrom(src => src.text_data))
      .ForMember(dest => dest.ParentValue , opt => opt.Ignore())
      .ForMember(dest => dest.InstanceId , opt => opt.MapFrom(src => src.instance_id))
      .ForMember(dest => dest.ValueInstanceId, opt => opt.MapFrom(src => src.value_instance_id))
      .ForMember(dest => dest.FieldId, opt => opt.MapFrom(src => src.field_id));  

      CreateMap<EntityFields, EntityFieldDto>()
      .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id))
      .ForMember(dest => dest.entity_id, opt => opt.MapFrom(src => src.EntityId))
      .ForMember(dest => dest.field_id, opt => opt.MapFrom(src => src.FieldId));  

       CreateMap<EntityFieldDto, EntityFields>()
      .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id))
      .ForMember(dest => dest.EntityId, opt => opt.MapFrom(src => src.entity_id))
      .ForMember(dest => dest.FieldId, opt => opt.MapFrom(src => src.field_id)); 

      CreateMap<ProjectType, ProjectTypeDto>()
      .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id))
      .ForMember(dest => dest.create_date, opt => opt.MapFrom(src => src.CreateDate.ToString("dd-MM-yyyy HH:mm")))
      .ForMember(dest => dest.decsription , opt => opt.MapFrom(src => src.Decsription))
      .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.Name));  

       CreateMap<ProjectTypeDto, ProjectType>()
      .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id))
      .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => Convert.ToDateTime(src.create_date, 	System.Globalization.CultureInfo.GetCultureInfo("ru-RU").DateTimeFormat)))
      .ForMember(dest => dest.Decsription, opt => opt.MapFrom(src => src.decsription))
      .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.name)); 

      CreateMap<ProjectTypeFields, ProjectTypeFieldsDto>()
      .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id))
      .ForMember(dest => dest.project_type_id, opt => opt.MapFrom(src => src.ProjectTypeId))
      .ForMember(dest => dest.field_id , opt => opt.MapFrom(src => src.FieldId));  

       CreateMap<ProjectTypeFieldsDto, ProjectTypeFields>()
      .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id))
      .ForMember(dest => dest.ProjectTypeId, opt => opt.MapFrom(src => src.project_type_id))
      .ForMember(dest => dest.FieldId, opt => opt.MapFrom(src => src.field_id)); 

      CreateMap<TaskValue, TaskValueDto>()
      .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id))
      .ForMember(dest => dest.task_id, opt => opt.MapFrom(src => src.TaskId))
      .ForMember(dest => dest.bool_data, opt => opt.MapFrom(src => src.BoolData))
      .ForMember(dest => dest.numeric_data, opt => opt.MapFrom(src => src.NumericData))
      .ForMember(dest => dest.instance_id, opt => opt.MapFrom(src => src.InstanceId))
      .ForMember(dest => dest.text_data, opt => opt.MapFrom(src => src.TextData))
      .ForMember(dest => dest.value_id, opt => opt.MapFrom(src => src.ValueId))
      .ForMember(dest => dest.field_id, opt => opt.MapFrom(src => src.FieldId));  

       CreateMap<TaskValueDto, TaskValue>()
      .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id))
      .ForMember(dest => dest.TaskId, opt => opt.MapFrom(src => src.task_id))
      .ForMember(dest => dest.BoolData, opt => opt.MapFrom(src => src.bool_data))
      .ForMember(dest => dest.NumericData, opt => opt.MapFrom(src => src.numeric_data))
      .ForMember(dest => dest.TextData, opt => opt.MapFrom(src => src.text_data))
      .ForMember(dest => dest.InstanceId , opt => opt.MapFrom(src => src.instance_id))
      .ForMember(dest => dest.ValueId , opt => opt.MapFrom(src => src.value_id))
      .ForMember(dest => dest.FieldId, opt => opt.MapFrom(src => src.field_id));  

      CreateMap<UserForRegistrationDto, ApplicationUser>()
        .ForMember(u => u.FullName, opt => opt.MapFrom(x => x.LastName+" "+x.FirstName ))
        .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email ))
        .ForMember(u => u.ParentId, opt => opt.MapFrom(x => x.ParentId ));
     
   }  
}
}