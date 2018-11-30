# Designs for REST Helpers

#### Simple controller with weak ETags and Status Codes
```cs
public class BasicController : Controller
{
    public BasicController(RestService<DataModel> restService)
    {
        this.RestService = restService;
    }

    public RestService<DataModel> RestService { get; }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        return RestService
            .Get(id, dataRepository.GetById)
            .UseWeakETags();
    }
}
```

#### Complex controller with separate Data, DTO and Patch models, strong and weak ETags, and Status Codes
```cs
public class ComplexController : Controller
{
    public ComplexController(RestService<DataModel, DtoModel, PatchModel> restService, IDataRepository dataRepository, IMapper mapper)
    {
        this.DataRepository = dataRepository;

        this.RestService = restService;
        this.RestService.DefaultWeakETags();
        this.RestService.DefaultStrongETags(d => d.ConcurrencyCheck);
        this.RestService.DefaultLoad(id => dataRepository.GetById);
        this.RestService.DefaultDtoMap(d => Mapper.Map<DtoModel>(d));
        this.RestService.DefaultPatchMap((u, d) => Mapper.Map(u, d));
        this.RestService.DefaultDataMap(d => Mapper.Map<DataModel>(d));

        this.Mapper = mapper;
    }

    public RestService<DataModel, DtoModel, PatchModel> RestService { get; }
    public IMapper Mapper { get; }
    public DataRepository DataRepository { get; }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        return RestService
            .Get(id);
    }

    [HttpPatch("{id}")]
    public IActionResult Patch(int id, PatchModel update)
    {
        return RestService
            .Patch(id, dataRepository.Update);
    }

    [HttpPost("")]
    public IActionResult Post(DtoModel model)
    {
        return RestService
            .Post(dataRepository.Create);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        return RestService
            .Delete(id, dataRepository.DeleteById);
    }
}
```