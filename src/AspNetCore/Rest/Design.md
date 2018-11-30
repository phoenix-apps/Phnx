# Designs for REST Helpers

## Sample data models
```cs
public class DataModel
{
    public int Id { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    // Detected by RestService to know that this data model supports strong ETags
    [ConcurrencyCheck]
    public string ConcurrencyCheck { get; set; }
}

public class DtoModel
{
    public int Id { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }
}

public class PatchModel
{
    public string FirstName { get; set; }

    public string LastName { get; set; }
}
```

## Constructor-based configuration
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
            .Get(id, dataRepository.GetById);
    }
}
```

#### Complex controller with separate Data, DTO and Patch models, support for both strong and weak ETags, and Status Codes
```cs
public class ComplexController : Controller
{
    public ComplexController(RestService<DataModel, DtoModel, PatchModel> restService, IDataRepository dataRepository, IMapper mapper)
    {
        this.RestService = restService;
        this.DataRepository = dataRepository;
        this.Mapper = mapper;
    }

    public RestService<DataModel, DtoModel, PatchModel> RestService { get; }
    public IMapper Mapper { get; }
    public DataRepository DataRepository { get; }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        return RestService
            .Get(id, DataRepository.GetById, Mapper.Map<DtoModel>);
    }

    [HttpPatch("{id}")]
    public IActionResult Patch(int id, PatchModel update)
    {
        return RestService
            .Patch(id, dataRepository.GetById, Mapper.Map, dataRepository.Update, Mapper.Map<DtoModel>);
    }

    [HttpPost("")]
    public IActionResult Post(DtoModel model)
    {
        return RestService
            .Post(Mapper.Map<DataModel>, dataRepository.Create, Mapper.Map<DtoModel>);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        return RestService
            .Delete(id, dataRepository.GetById, dataRepository.DeleteById);
    }
}
```

## Minimal Helper configuration
```cs
public class ComplexController : Controller
{
    public ComplexController(IRestRequest restRequest, IRestResponse restResponse, IDataRepository dataRepository)
    {
        RestRequest = restRequest;
        RestResponse = restResponse;
        DataRepository = dataRepository;
    }

    public IRestRequest RestRequest { get; }
    public IRestResponse RestResponse { get; }

    public IDataRepository DataRepository { get; }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var dataModel = DataRepository.GetById(id);

        if (dataModel is null)
        {
            return RestResponse.NotFound();
        }

        if (!RestRequest.ShouldGet(dataModel.ConcurrencyStamp))
        {
            return RestResponse.ShouldntGet();
        }

        // Map
        var mapped = Mapper.Map<DtoModel>(dataModel);

        return RestResponse.Ok(mapped, dataModel.ConcurrencyStamp);
    }

    [HttpPost("")]
    public IActionResult Post(DtoModel model)
    {
        var data = Mapper.Map<DataModel>(model);

        DataRepository.Create(data);

        var dto = Mapper.Map<DtoModel>(model);

        return RestResponse.CreatedAt(dto, data.ConcurrencyStamp, "Post", "Complex", null);
    }

    [HttpPatch("{id}")]
    public IActionResult Patch(PatchModel model)
    {
        var dataModel = DataRepository.GetById(id);

        if (dataModel is null)
        {
            return RestResponse.NotFound();
        }

        if (!RestRequest.ShouldUpdate(dataModel.ConcurrencyStamp))
        {
            return RestResponse.ShouldntUpdate();
        }

        var data = Mapper.Map<DataModel>(model, dataModel);

        DataRepository.Update(data);

        var dto = Mapper.Map<DtoModel>(model);

        return RestResponse.Updated(dto, data.ConcurrencyStamp);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var dataModel = DataRepository.GetById(id);

        if (!RestRequest.ShouldDelete(dataModel.ConcurrencyStamp))
        {
            return RestResponse.ShouldntDelete();
        }

        // Map
        var mapped = Mapper.Map<DtoModel>(dataModel);

        return RestResponse.Deleted();
    }
}
```

## Convention Based Helper (Requires AutoMapper and EF Core)
```cs

public class ComplexController : Controller
{
    public ComplexController(RestService<DataModel, DtoModel, PatchModel> restService)
    {
        // Internally loads the matching DbSet<DataModel>, and uses AutoMapper to map between the DatModel, DtoModel and PatchModel
        this.RestService = restService;
    }

    public RestService<DataModel, DtoModel, PatchModel> RestService { get; }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        return RestService.Get(id);
    }

    [HttpPatch("{id}")]
    public IActionResult Patch(int id, PatchModel update)
    {
        return RestService.Patch(id);
    }

    [HttpPost("")]
    public IActionResult Post(DtoModel model)
    {
        return RestService.Post();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        return RestService.Delete();
    }
}
```