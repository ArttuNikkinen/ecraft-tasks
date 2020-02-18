import React, { Component } from 'react';
import data from '../build-front/data/meetings.json';

//This component show the current active meeting.
//This component does not work as intended.
//At the moment the information is hard-coded fetch from the json file.
//The intended functionality is to check from the json file if there are
//meeting items that would actually be active during the current time.

class CurrentMeeting extends Component {
    render() {
        return (
            <div className="CurrentMeeting">
                <h3>Current Meeting</h3>
                <h1 className="CurrentMeetingHeader">
                    {data.map((entry, index) => {
                        if(entry.Participants !== null)
                        {
                            return entry.Subject.toUpperCase();
                        }
                    })}
                </h1>

                    {data.map((entry, index) => {
                        if(entry.Participants !== null)
                        {
                            let start = entry.StartTime
                            let end = entry.EndTime
                            return start.slice(11,16)+ " - "+ end.slice(11,16)
                        }
                    })}<br />

                    {data.map((entry, index) => {
                        if(entry.Participants !== null)
                        {
                            return entry.Organizer.toUpperCase();
                        }
                    })}
            </div>
        )
    }
}
export default CurrentMeeting;