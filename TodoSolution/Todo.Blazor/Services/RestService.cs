using AutoMapper;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using Todo.Dto;
using Todo.Blazor.Models;

namespace Todo.Blazor.Services;

public class RestService : IRestService
{
    private readonly HttpClient _httpClient;
    private JsonSerializerOptions _serializerOptions;
    private readonly IMapper _mapper;

    public List<TodoItem>? Items { get; private set; }

    public RestService(HttpClient httpClient, IMapper mapper)
    {
        _mapper = mapper;
        _httpClient = httpClient;
        _serializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, WriteIndented = true };
    }

    public async Task<List<TodoItem>> GetTodoItemsAsync()
    {
        // The single line below is enough for the entire method body, but then without detailed error checking
        //return _mapper.Map<List<TodoItem>> (await _httpClient.GetFromJsonAsync<List<TodoItemResponseDto>>("/api/todoitems", _serializerOptions) ?? []);
        
        Items = new List<TodoItem>();
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync("/api/todoitems");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                Items = _mapper.Map<List<TodoItem>>(JsonSerializer.Deserialize<List<TodoItemResponseDto>>(content, _serializerOptions));
                // The two lines above can be replaced with the single line below
                //Items = _mapper.Map<List<TodoItem>>(await response.Content.ReadFromJsonAsync<List<TodoItemResponseDto>>(_serializerOptions));
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }
        return Items;
    }

    // This method shows a one-line implementation, but lacks detailed error checking
    public async Task<TodoItem> GetTodoItemAsync(int id)
        => _mapper.Map<TodoItem> (await _httpClient.GetFromJsonAsync<TodoItemResponseDto>($"/api/todoitems/{id}", _serializerOptions) ?? throw new Exception("Could not find TodoItem"));

    public async Task AddTodoItemAsync(TodoItem todoItem)
    {
        // The single line below is enough for the entire method body, but then without detailed error checking
        //await _httpClient.PostAsJsonAsync("/api/todoitems", _mapper.Map<TodoItemRequestDto>(todoItem), _serializerOptions);

        try
        {
            string json = JsonSerializer.Serialize<TodoItemRequestDto>(_mapper.Map<TodoItemRequestDto>(todoItem), _serializerOptions);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null!;
            response = await _httpClient.PostAsync($"/api/todoitems", content);
            if (response.IsSuccessStatusCode)
                Debug.WriteLine(@"\tTodoItem successfully created.");
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }
    }

    public async Task UpdateTodoItemAsync(TodoItem todoItem)
    {
        // The single line below is enough for the entire method body, but then without detailed error checking
        //await _httpClient.PutAsJsonAsync($"/api/todoitems/{todoItem.Id}", _mapper.Map<TodoItemRequestDto>(todoItem), _serializerOptions);

        try
        {
            string json = JsonSerializer.Serialize<TodoItemRequestDto>(_mapper.Map<TodoItemRequestDto>(todoItem), _serializerOptions);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null!;
            response = await _httpClient.PutAsync($"/api/todoitems/{todoItem.Id}", content);
            if (response.IsSuccessStatusCode)
                Debug.WriteLine(@"\tTodoItem successfully updated.");
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }
    }

    public async Task DeleteTodoItemAsync(int id)
    {
        // The single line below is enough for the entire method body, but then without detailed error checking
        //await _httpClient.DeleteAsync($"/api/todoitems/{id}");

        try
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"/api/todoitems/{id}");
            if (response.IsSuccessStatusCode)
                Debug.WriteLine(@"\tTodoItem successfully deleted.");
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }
    }
}