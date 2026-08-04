using BusinessLayer.Common;
using BusinessLayer.DTOs;


namespace BusinessLayer.Interfaces
{
    public interface IBreakPolicyService
    {
        Task<ApiResponse<BreakPolicyDto>> CreateAsync(BreakPolicyCreateDto dto);

        Task<ApiResponse<BreakPolicyDto>> UpdateAsync(BreakPolicyUpdateDto dto);

        Task<ApiResponse<BreakPolicyDto>> DeleteAsync(BreakPolicyDeleteDto dto);

        Task<List<BreakPolicyDto>> GetAllAsync(int userId);

        Task<BreakPolicyDto> GetByIdAsync(int breakPolicyId);

        Task<BreakResponseDto> BreakInAsync(BreakInDto dto);

        Task<BreakResponseDto> BreakOutAsync(BreakOutDto dto);
    }
}
