using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Logging.Events;
using System.Linq;

namespace BindOpen.Logging
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class BdoLogConverter
    {
        // Serialization ----------------------------

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static LogDto ToDto(this IBdoCompleteLog poco)
        {
            if (poco == null) return null;

            LogDto dto = new()
            {
                Description = poco.Description,
                Detail = poco.Detail.ToDto(),
                Title = poco.Title,
                Events = poco._Events?.Select(q => q.ToDto()).ToList(),
                Execution = poco.Execution.ToDto(),
                Task = poco.TaskConfig.ToDto()
            };

            return dto;
        }

        /// <summary>
        /// Converts to POCO.
        /// </summary>
        /// <param name="dto">The dto to consider.</param>
        /// <returns>The POCO object.</returns>
        public static IBdoCompleteLog ToPoco(this LogDto dto)
        {
            if (dto == null) return null;

            BdoLog poco = new();
            poco
                .WithEvents(dto.Events?.Select(q => q.ToPoco()).ToArray())
                .WithExecution(dto.Execution.ToPoco())
                .WithTask(dto.Task.ToPoco())
                .WithDescription(dto.Description)
                .WithDetail(dto.Detail.ToPoco())
                .WithTitle(dto.Title);

            return poco;
        }
    }
}
