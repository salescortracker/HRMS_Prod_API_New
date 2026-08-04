using BusinessLayer.Common;
using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
using DataAccessLayer.DBContext;
using DataAccessLayer.Repositories.GeneralRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Implementations
{
    public class BreakPolicyService: IBreakPolicyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly HRMSContext _context;

        public BreakPolicyService(IUnitOfWork unitOfWork,HRMSContext hRMSContext)
        {
            _unitOfWork = unitOfWork;
            _context = hRMSContext;
        }

        public async Task<Common.ApiResponse<BreakPolicyDto>> CreateAsync(BreakPolicyCreateDto dto)
        {
            try
            {
                var policies = await _unitOfWork.Repository<BreakPolicy>().GetAllAsync();

                var duplicate = policies.FirstOrDefault(x =>
                    !x.IsDeleted &&
                    x.CompanyId == dto.CompanyId &&
                    x.RegionId == dto.RegionId &&
                    x.PolicyCode == dto.PolicyCode);

                if (duplicate != null)
                {
                    return new Common.ApiResponse<BreakPolicyDto>()
                    {
                        Success = false,
                        Message = "Policy Code already exists."
                    };
                }

                BreakPolicy entity = new BreakPolicy();

                entity.CompanyId = dto.CompanyId;
                entity.RegionId = dto.RegionId;
                entity.UserId = dto.UserId;
                entity.PolicyCode = dto.PolicyCode;
                entity.PolicyName = dto.PolicyName;
                entity.BreakType = dto.BreakType;
                entity.BreakFromTime = dto.BreakFromTime;
                entity.BreakToTime = dto.BreakToTime;
                entity.DurationMinutes = dto.DurationMinutes;
                entity.MaxBreaksPerDay = dto.MaxBreaksPerDay;
                entity.GraceMinutes = dto.GraceMinutes;
                entity.ShiftId = dto.ShiftId;
                entity.IsActive = dto.IsActive;

                entity.IsDeleted = false;
                entity.CreatedBy = dto.UserId;
                entity.CreatedDate = DateTime.Now;
                entity.UpdatedBy = dto.UserId;
                entity.UpdatedDate = DateTime.Now;

                await _unitOfWork.Repository<BreakPolicy>().AddAsync(entity);

                await _unitOfWork.CompleteAsync();

                return new Common.ApiResponse<BreakPolicyDto>
                {
                    Success = true,
                    Message = "Break Policy created successfully."
                };
            }
            catch (Exception ex)
            {
                return new Common.ApiResponse<BreakPolicyDto>
                {
                    Success = false,
                    Message = ex.Message
                };
            }

        }
        public async Task<Common.ApiResponse<BreakPolicyDto>> UpdateAsync(BreakPolicyUpdateDto dto)
        {
            try
            {
                var breakPolicies = await _unitOfWork.Repository<BreakPolicy>().GetAllAsync();

                var duplicate = breakPolicies.FirstOrDefault(x =>
                    x.BreakPolicyId != dto.BreakPolicyId &&
                    !x.IsDeleted &&
                    x.CompanyId == dto.CompanyId &&
                    x.RegionId == dto.RegionId &&
                    x.PolicyCode == dto.PolicyCode);

                if (duplicate != null)
                {
                    return new Common.ApiResponse<BreakPolicyDto>()
                    {
                        Success = false,
                        Message = "Policy Code already exists."
                    };
                }

                var entity = breakPolicies.FirstOrDefault(x =>
                    x.BreakPolicyId == dto.BreakPolicyId &&
                    !x.IsDeleted);

                if (entity == null)
                {
                    return new Common.ApiResponse<BreakPolicyDto>()
                    {
                        Success = false,
                        Message = "Break Policy not found."
                    };
                }

                entity.CompanyId = dto.CompanyId;
                entity.RegionId = dto.RegionId;
                entity.UserId = dto.UserId;
                entity.PolicyCode = dto.PolicyCode;
                entity.PolicyName = dto.PolicyName;
                entity.BreakType = dto.BreakType;
                entity.DurationMinutes = dto.DurationMinutes;
                entity.BreakFromTime = dto.BreakFromTime;
                entity.BreakToTime = dto.BreakToTime;
                entity.MaxBreaksPerDay = dto.MaxBreaksPerDay;
                entity.GraceMinutes = dto.GraceMinutes;
                entity.ShiftId = dto.ShiftId;
                entity.IsActive = dto.IsActive;

                entity.UpdatedBy = dto.UserId;
                entity.UpdatedDate = DateTime.Now;

                _unitOfWork.Repository<BreakPolicy>().Update(entity);

                await _unitOfWork.CompleteAsync();

                return new Common.ApiResponse<BreakPolicyDto>()
                {
                    Success = true,
                    Message = "Break Policy updated successfully."
                };
            }
            catch (Exception ex)
            {
                return new Common.ApiResponse<BreakPolicyDto>()
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
        public async Task<Common.ApiResponse<BreakPolicyDto>> DeleteAsync(BreakPolicyDeleteDto dto)
        {
            try
            {
                var entity = await _unitOfWork.Repository<BreakPolicy>()
                    .GetByIdAsync(dto.BreakPolicyId);

                if (entity == null || entity.IsDeleted)
                {
                    return new Common.ApiResponse<BreakPolicyDto>()
                    {
                        Success = false,
                        Message = "Break Policy not found."
                    };
                }

                entity.IsDeleted = true;
                entity.IsActive = false;
                entity.UpdatedBy = dto.UserId;
                entity.UpdatedDate = DateTime.Now;

                _unitOfWork.Repository<BreakPolicy>().Update(entity);

                await _unitOfWork.CompleteAsync();

                return new Common.ApiResponse<BreakPolicyDto>()
                {
                    Success = true,
                    Message = "Break Policy deleted successfully."
                };
            }
            catch (Exception ex)
            {
                return new Common.ApiResponse<BreakPolicyDto>()
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
        public async Task<BreakPolicyDto> GetByIdAsync(int breakPolicyId)
        {
            var data =
                (from bp in await _unitOfWork.Repository<BreakPolicy>().GetAllAsync()

                 join c in await _unitOfWork.Repository<Company>().GetAllAsync()
                    on bp.CompanyId equals c.CompanyId

                 join r in await _unitOfWork.Repository<Region>().GetAllAsync()
                    on bp.RegionId equals r.RegionId

                 join s in await _unitOfWork.Repository<ShiftMaster>().GetAllAsync()
                    on bp.ShiftId equals s.ShiftId

                 where bp.BreakPolicyId == breakPolicyId
                       && !bp.IsDeleted

                 select new BreakPolicyDto
                 {
                     BreakPolicyId = bp.BreakPolicyId,

                     CompanyId = bp.CompanyId,
                     CompanyName = c.CompanyName,

                     RegionId = bp.RegionId,
                     RegionName = r.RegionName,

                     UserId = bp.UserId,

                     PolicyCode = bp.PolicyCode,
                     PolicyName = bp.PolicyName,

                     BreakType = bp.BreakType,

                     DurationMinutes = bp.DurationMinutes,
                     MaxBreaksPerDay = bp.MaxBreaksPerDay,
                     GraceMinutes = bp.GraceMinutes,

                     ShiftId = bp.ShiftId,
                     ShiftName = s.ShiftName,

                     IsActive = bp.IsActive

                 }).FirstOrDefault();

            return data;
        }
        public async Task<List<BreakPolicyDto>> GetAllAsync(int userId)
        {
            var result =
                (from bp in await _unitOfWork.Repository<BreakPolicy>().GetAllAsync()

                 join c in await _unitOfWork.Repository<Company>().GetAllAsync()
                    on bp.CompanyId equals c.CompanyId

                 join r in await _unitOfWork.Repository<Region>().GetAllAsync()
                    on bp.RegionId equals r.RegionId

                 //join s in await _unitOfWork.Repository<ShiftMaster>().GetAllAsync()
                 //   on bp.ShiftId equals s.ShiftId

                 where bp.UserId == userId

                 orderby bp.PolicyName

                 select new BreakPolicyDto
                 {
                     BreakPolicyId = bp.BreakPolicyId,

                     CompanyId = bp.CompanyId,
                     CompanyName = c.CompanyName,

                     RegionId = bp.RegionId,
                     RegionName = r.RegionName,

                     UserId = bp.UserId,

                     PolicyCode = bp.PolicyCode,
                     PolicyName = bp.PolicyName,

                     BreakType = bp.BreakType,

                     DurationMinutes = bp.DurationMinutes,
                     MaxBreaksPerDay = bp.MaxBreaksPerDay,
                     GraceMinutes = bp.GraceMinutes,

                     //ShiftId = bp.ShiftId,
                     //ShiftName = s.ShiftName,

                     IsActive = bp.IsActive

                 }).Where(x => x.UserId == userId).ToList();

            return result;
        }
        public async Task<BreakResponseDto> BreakInAsync(BreakInDto dto)
        {
            try
            {
                var today = DateOnly.FromDateTime(DateTime.Now);

                // Get today's attendance
                var attendance = await _context.ClockInOuts
                    .FirstOrDefaultAsync(x =>
                        x.CompanyId == dto.CompanyId &&
                        x.RegionId == dto.RegionId &&
                        x.CreatedBy == dto.UserId &&
                        x.AttendanceDate == today &&
                        x.ClockOutTime == null);

                if (attendance == null)
                {
                    return new BreakResponseDto
                    {
                        Success = false,
                        Message = "Employee is not clocked in."
                    };
                }

                // Check existing open break
                var activeBreak =  _context.BreakLogs
                    .Where(x =>
                        x.ClockInOutId == attendance.ClockInOutId &&
                        x.BreakOutTime == null).FirstOrDefault();

                if (activeBreak != null)
                {
                    return new BreakResponseDto
                    {
                        Success = false,
                        Message = "Employee is already on break."
                    };
                }

                // Get active policy
                var policy = await _context.BreakPolicies.OrderBy(x=>x.BreakFromTime)
                    .FirstOrDefaultAsync(x =>
                        x.CompanyId == dto.CompanyId &&
                        x.RegionId == dto.RegionId  &&
                        x.IsActive &&
                        !x.IsDeleted);

                if (policy == null)
                {
                    return new BreakResponseDto
                    {
                        Success = false,
                        Message = "Break policy not assigned."
                    };
                }

                var currentTime = TimeOnly.FromDateTime(DateTime.Now);

                // Validate break window
                if (currentTime < policy.BreakFromTime ||
                    currentTime > policy.BreakToTime)
                {
                    return new BreakResponseDto
                    {
                        Success = false,
                        Message = $"Break allowed only between {policy.BreakFromTime:hh\\:mm} and {policy.BreakToTime:hh\\:mm}."
                    };
                }

                // Count today's breaks
                var breakCount = await _context.BreakLogs
                    .CountAsync(x =>
                        x.ClockInOutId == attendance.ClockInOutId 
                        );

                if (breakCount >= policy.MaxBreaksPerDay)
                {
                    return new BreakResponseDto
                    {
                        Success = false,
                        Message = "Maximum break limit reached."
                    };
                }

                // Create Break Log
                var breakLog = new BreakLog
                {
                    ClockInOutId = attendance.ClockInOutId,
                    CompanyId = dto.CompanyId,
                    RegionId = dto.RegionId,
                    UserId = dto.UserId,
                    BreakPolicyId = policy.BreakPolicyId,

                    BreakInTime = DateTime.Now,

                    Status = "Break In",

                    CreatedBy = dto.UserId,
                    CreatedAt = DateTime.Now,

                    IsDeleted = false
                };

                _context.BreakLogs.Add(breakLog);

                await _context.SaveChangesAsync();

                return new BreakResponseDto
                {
                    Success = true,
                    Message = "Break started successfully.",

                    BreakLogId = breakLog.BreakLogId,

                    BreakInTime = breakLog.BreakInTime,

                    BreakMinutes = 0,

                    IsExceeded = false
                };
            }
            catch (Exception ex)
            {
                return new BreakResponseDto
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<BreakResponseDto> BreakOutAsync(BreakOutDto dto)
        {
            try
            {
                var today = DateOnly.FromDateTime(DateTime.Now);

                // Get today's active attendance
                var attendance = await _context.ClockInOuts
                    .FirstOrDefaultAsync(x =>
                        x.CompanyId == dto.CompanyId &&
                        x.RegionId == dto.RegionId &&
                        x.CreatedBy == dto.UserId &&
                        x.AttendanceDate == today &&
                        x.ClockOutTime == null);

                if (attendance == null)
                {
                    return new BreakResponseDto
                    {
                        Success = false,
                        Message = "Employee is not clocked in."
                    };
                }

                // Get active break
                var breakLog = await _context.BreakLogs
                    .Where(x =>
                        x.ClockInOutId == attendance.ClockInOutId)
                    .OrderByDescending(x => x.BreakInTime)
                    .FirstOrDefaultAsync();

                if (breakLog == null)
                {
                    return new BreakResponseDto
                    {
                        Success = false,
                        Message = "No active break found."
                    };
                }

                // Get break policy
                var policy = await _context.BreakPolicies
                    .FirstOrDefaultAsync(x =>
                        x.BreakPolicyId == breakLog.BreakPolicyId &&
                        x.IsActive &&
                        !x.IsDeleted);

                if (policy == null)
                {
                    return new BreakResponseDto
                    {
                        Success = false,
                        Message = "Break policy not found."
                    };
                }

                // Break Out Time
                breakLog.BreakOutTime = DateTime.Now;

                // Calculate Break Minutes
                breakLog.BreakMinutes =
                    (int)(breakLog.BreakOutTime.Value - breakLog.BreakInTime).TotalMinutes;

                // Check if exceeded allowed duration
                int? allowedMinutes = policy.DurationMinutes + policy.GraceMinutes;

               // breakLog.is = breakLog.BreakMinutes > allowedMinutes;

                breakLog.Status = "Break Out";
                breakLog.ModifiedBy = dto.UserId;
                breakLog.ModifiedAt = DateTime.Now;

                _context.BreakLogs.Update(breakLog);

                await _context.SaveChangesAsync();

                return new BreakResponseDto
                {
                    Success = true,
                    Message = 
                         $"Break ended. Allowed break exceeded by {breakLog.BreakMinutes - allowedMinutes} minute(s).",
                       

                    BreakLogId = breakLog.BreakLogId,
                    BreakInTime = breakLog.BreakInTime,
                    BreakOutTime = breakLog.BreakOutTime,
                    BreakMinutes = breakLog.BreakMinutes ?? 0,
                   // IsExceeded = breakLog.IsExceeded
                };
            }
            catch (Exception ex)
            {
                return new BreakResponseDto
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}
