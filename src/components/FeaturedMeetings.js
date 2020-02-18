import React, { Component } from 'react';
import data from '../build-front/data/meetings.json'

//This component display the featured meetings that are happening today in the bottom of the left panel.
//This component does not function as intended. 
//Each meeting is hard-coded fetch from the json file.
//This component should get the information from the json file
//and check which meetings are happening today

class FeaturedMeetings extends Component {
    render() {
        return(
            <div className="FeaturedMeetings">
                <table className="FeaturedTable">
                    <tbody>
                    <tr>
                        <th className="FeaturedTime1"> 
                        {data.map((entry, index) => {
                            if(entry.Participants === null && index === 1)      //This <th> gets the time item from the meetings.json file
                            {                                                   //There are three times to display and this is done three times total
                                let start = entry.StartTime
                                let end = entry.EndTime
                                return start.slice(11,16)+ " - "+ end.slice(11,16)
                            }
                        })}
                        </th>

                        <th className="FeaturedTime2">
                        {data.map((entry, index) => {
                            if(entry.Participants === null && index === 2)
                            {
                                let start = entry.StartTime
                                let end = entry.EndTime
                                return start.slice(11,16)+ " - "+ end.slice(11,16)
                            }
                        })}
                        </th>

                        <th className="FeaturedTime3">
                        {data.map((entry, index) => {
                            if(entry.Participants === null && index === 3)
                            {
                                let start = entry.StartTime
                                let end = entry.EndTime
                                return start.slice(11,16)+ " - "+ end.slice(11,16)
                            }
                        })}
                        </th>
                    </tr>
                    
                
    
                <tr>
                    <td className="FeaturedSubject1">
                    {data.map((entry, index) => {                           //This gets the title of the meetings
                        if(entry.Participants === null && index === 1)      //Similar to the FeaturedTime earlier
                        {
                            return  entry.Subject
                        }
                 })}
                 </td>

                 <td className="FeaturedSubject2">
                    {data.map((entry, index) => {
                        if(entry.Participants === null && index === 2)
                        {
                            return  entry.Subject
                        }
                 })}
                 </td>

                 <td className="FeaturedSubject3">
                    {data.map((entry, index) => {
                        if(entry.Participants === null && index === 3)
                        {
                            return  entry.Subject
                        }
                 })}
                 </td>
                 </tr>
                 
                 
                 <tr>
                    <td className="FeaturedOrganizer1">
                    {data.map((entry, index) => {                       //This gets the organizer of the meeting
                        if(entry.Participants === null && index === 1)  //This works similar to earlier data maps
                        {
                            return  entry.Organizer.toUpperCase();
                        }
                 })}
                 </td>

                 <td className="FeaturedOrganizer2">
                    {data.map((entry, index) => {
                        if(entry.Participants === null && index === 2)
                        {
                            return  entry.Organizer.toUpperCase();
                        }
                 })}
                 </td>

                 <td className="FeaturedOrganizer3">
                    {data.map((entry, index) => {
                        if(entry.Participants === null && index === 3)
                        {
                            return  entry.Organizer.toUpperCase();
                        }
                 })}
                 </td>
                 </tr>
                 </tbody>
                 </table>
            </div>
        )
    }
}
export default FeaturedMeetings;