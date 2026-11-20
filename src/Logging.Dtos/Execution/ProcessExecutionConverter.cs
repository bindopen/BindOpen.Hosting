using BindOpen.Data;
using BindOpen.Data.Helpers;

namespace BindOpen.Logging
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class ProcessExecutionConverter
    {
        // Serialization ----------------------------

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static ProcessExecutionDto ToDto(this IBdoProcessExecution poco)
        {
            if (poco == null) return null;

            ProcessExecutionDto dto = new()
            {
                CustomStatus = poco.CustomStatus,
                EndDate = poco.EndDate.ToString(DataValueTypes.Date),
                Location = poco.Location,
                ProgressIndex = poco.ProgressIndex,
                ProgressMax = poco.ProgressMax,
                RestartDate = poco.RestartDate.ToString(DataValueTypes.Date),
                ResultLevel = poco.ResultLevel,
                StartDate = poco.StartDate.ToString(DataValueTypes.Date),
                State = poco.State,
                Status = poco.Status
            };

            return dto;
        }

        /// <summary>
        /// Converts to POCO.
        /// </summary>
        /// <param name="dto">The dto to consider.</param>
        /// <returns>The POCO object.</returns>
        public static IBdoProcessExecution ToPoco(this ProcessExecutionDto dto)
        {
            if (dto == null) return null;

            ProcessExecution poco = new()
            {
                CustomStatus = dto.CustomStatus,
                EndDate = dto.EndDate.ToDateTime(),
                Location = dto.Location,
                ProgressIndex = dto.ProgressIndex,
                ProgressMax = dto.ProgressMax,
                RestartDate = dto.RestartDate.ToDateTime(),
                ResultLevel = dto.ResultLevel,
                StartDate = dto.StartDate.ToDateTime(),
                State = dto.State,
                Status = dto.Status
            };

            return poco;
        }
    }
}
