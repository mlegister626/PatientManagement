using PatientApi.Dtos;
using PatientApi.Entities;
using PatientApi.Exceptions;
using PatientApi.Repositories;

namespace PatientApi.Services;

public class MealDeliveryService : IMealDeliveryService
{
    private readonly IMealDeliveryRepository _mealDeliveryRepository;

    public MealDeliveryService(IMealDeliveryRepository mealDeliveryRepository)
    {
        _mealDeliveryRepository = mealDeliveryRepository;
    }

    public async Task<IEnumerable<MealDeliveryDto>> ListMealDeliveriesAsync()
    {
        var mealDeliveries = await _mealDeliveryRepository.GetAllAsync();
        return mealDeliveries.Select(MapToDto);
    }

    public async Task<MealDeliveryDto> GetMealDeliveryByIdAsync(int id)
    {
        var mealDelivery = await _mealDeliveryRepository.GetByIdAsync(id);
        if (mealDelivery is null)
        {
            throw new NotFoundException($"Meal delivery with ID {id} not found.");
        }

        return MapToDto(mealDelivery);
    }

    public async Task<MealDeliveryDto> CreateMealDeliveryAsync(CreateMealDeliveryDto dto)
    {
        var entity = new MealDelivery
        {
            PatientId = dto.PatientId,
            FoodId = dto.FoodId,
            PortionGiven = dto.PortionGiven,
            MealType = dto.MealType,
            DateDelivered = dto.DateDelivered
        };

        var created = await _mealDeliveryRepository.AddAsync(entity);

        // Re-fetch so the Food navigation property is loaded for the DTO mapping.
        var withFood = await _mealDeliveryRepository.GetByIdAsync(created.MealDeliveryId);
        return MapToDto(withFood!);
    }

    public async Task<MealDeliveryDto> UpdateMealDeliveryAsync(int id, UpdateMealDeliveryDto dto)
    {
        var existing = await _mealDeliveryRepository.GetByIdAsync(id);
        if (existing is null)
        {
            throw new NotFoundException($"Meal delivery with ID {id} not found.");
        }

        var entity = new MealDelivery
        {
            MealDeliveryId = id,
            PatientId = dto.PatientId,
            FoodId = dto.FoodId,
            PortionGiven = dto.PortionGiven,
            MealType = dto.MealType,
            DateDelivered = dto.DateDelivered
        };

        await _mealDeliveryRepository.UpdateAsync(entity);

        var updated = await _mealDeliveryRepository.GetByIdAsync(id);
        return MapToDto(updated!);
    }

    public async Task<bool> DeleteMealDeliveryAsync(int id)
    {
        var existing = await _mealDeliveryRepository.GetByIdAsync(id);
        if (existing is null)
        {
            throw new NotFoundException($"Meal delivery with ID {id} not found.");
        }

        return await _mealDeliveryRepository.DeleteAsync(id);
    }

    public async Task<ICollection<MealDeliveryDto>> GetMealDeliveriesByPatientIdAsync(int patientId)
    {
        var mealDeliveries = await _mealDeliveryRepository.GetByPatientIdAsync(patientId);
        return mealDeliveries.Select(MapToDto).ToList();
    }

    private static MealDeliveryDto MapToDto(MealDelivery mealDelivery)
    {
        // Food.Calories requires the repository to have Include()'d Food -
        // both GetAllAsync/GetByIdAsync/GetByPatientIdAsync do this.
        var calories = mealDelivery.Food?.Calories ?? 0;

        return new MealDeliveryDto
        {
            MealDeliveryId = mealDelivery.MealDeliveryId,
            PatientId = mealDelivery.PatientId,
            FoodId = mealDelivery.FoodId,
            FoodName = mealDelivery.Food?.Name ?? string.Empty,
            PortionGiven = mealDelivery.PortionGiven,
            TotalCalories = mealDelivery.PortionGiven * calories,
            MealType = mealDelivery.MealType,
            DateDelivered = mealDelivery.DateDelivered
        };
    }
}
