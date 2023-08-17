using gantt_backend.Interfaces.Constructor;
namespace gantt_backend.Interfaces.UOF
{
public interface IUnitOfWork : IDisposable
{
    ILinkRepository Links { get; }
    ITaskRepository Tasks { get; }
    IUserRepository Users { get; }
    IAssignmentRepository Assignments { get; }
    IEntityRepository Entities {get;}
    IFieldRepository Fields { get; }
    IEntityFieldRepository EntityFields { get; }
    IInstanceRepository Instances { get; }
    IValueRepository Values {get;}
    IProjectTypeRepository ProjectTypes {get;}
    ITaskValueRepository TaskValues {get;} 
    IProjectTypeFieldsRepository ProjectTypeFields {get;}
    Task CompleteAsync();
}
}