namespace Spectrum.Content.Appointments.Services
{
    using Ical.Net;
    using Ical.Net.DataTypes;
    using Ical.Net.Interfaces.DataTypes;
    using Ical.Net.Serialization;
    using Ical.Net.Serialization.iCalendar.Serializers;
    using System;
    using System.Collections.Generic;
    using Models;
    using ViewModels;

    // ReSharper disable once InconsistentNaming
    public class ICalendarService : IICalendarService
    {
        /// <summary>
        /// Gets the i calendar string.
        /// </summary>
        /// <param name="eventViewModel"></param>
        /// <returns></returns>
        public ICalEventModel GetICalendarString(EventViewModel eventViewModel)
        {
            Event calendarEvent = new Event
            {
                DtStart = new CalDateTime(eventViewModel.StartTime),
                DtEnd = new CalDateTime(eventViewModel.EndTime),
                Summary =  eventViewModel.Summary,
                Description = eventViewModel.Description,
                Attendees = GetAttendees(eventViewModel.Attendees)
            };

            Calendar calendar = new Calendar();
            calendar.Events.Add(calendarEvent);

            CalendarSerializer serializer = new CalendarSerializer(new SerializationContext());
            string serializedCalendar = serializer.SerializeToString(calendar);

            return new ICalEventModel
            {
                Guid = calendarEvent.Uid,
                SerializedString = serializedCalendar
            };
        }

        /// <summary>
        /// Gets the attendees.
        /// </summary>
        /// <param name="attendees">The attendees.</param>
        /// <returns></returns>
        IList<IAttendee> GetAttendees(IList<string> attendees)
        {
            IList<IAttendee> attendeeList = new List<IAttendee>();

            foreach (string attendee in attendees)
            {
                attendeeList.Add(new Attendee { Value = new Uri("mailto:" + attendee) });
            }

            return attendeeList;
        }
    }
}