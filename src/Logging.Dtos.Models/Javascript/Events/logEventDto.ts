/* Auto Generated */

import { EventDto } from "./eventDto";
import { LogDto } from "./../Logs/logDto";

export interface LogEventDto extends EventDto {
    resultCode: string;
    source: string;
    stackTraces: any[];
    log: LogDto;
}
