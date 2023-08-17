using gantt_backend.Interfaces.UOF;
using gantt_backend.Data.DBContext;
using gantt_backend.Interfaces;
using gantt_backend.Interfaces.Constructor;
using AutoMapper;

namespace gantt_backend.Repositories.UOF
{
public class UnitOfWork : IUnitOfWork
{
    private readonly GanttContext _context;
    private readonly IMapper _mapper;
    public UnitOfWork (GanttContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        Links = new LinkRepository(_context);
        Tasks = new TaskRepository(_context);
        Users = new UserRepository(_context);
        Assignments = new AssignmentRepository(_context);
        Entities = new EntityRepository(_context);
        Fields = new FieldRepository(_context);
        EntityFields = new EntityFieldRepository(_context);
        Instances = new InstanceRepository(_context);
        Values = new ValueRepository(_context,mapper);
        TaskValues = new TaskValueRepository(_context,mapper);
        ProjectTypes = new ProjectTypeRepository(_context);
        ProjectTypeFields = new ProjectTypeFieldsRepository(_context);

    }
    public ITaskValueRepository TaskValues { get; private set; }
    public IProjectTypeRepository ProjectTypes { get; private set; }
    public ILinkRepository Links { get; private set; }
    public ITaskRepository Tasks { get; private set; }
    public IUserRepository Users { get; private set; }
    public IAssignmentRepository Assignments { get; private set; }
    public IEntityRepository Entities { get; private set; }
    public IFieldRepository Fields { get; private set; }
    public IEntityFieldRepository EntityFields { get; private set; }
    public IInstanceRepository Instances { get; private set; }
    public IValueRepository Values { get; private set; }
    public IProjectTypeFieldsRepository ProjectTypeFields { get; private set; }
     public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    public void Dispose()
    {
        _context.Dispose();
    }
}
}