import React, { Component } from 'react';
import { Scheduler, DayView, Appointments } from '@devexpress/dx-react-scheduler-material-ui';
import { ViewState } from '@devexpress/dx-react-scheduler';
import  meetings   from '../build-front/data/meetings.json';
import { MuiThemeProvider, createMuiTheme } from "@material-ui/core/styles";
import Clock from '../build-front/icons/time.png';
import Person from '../build-front/icons/person.png';
import Description from '../build-front/icons/description.png';


//Here we get the current month to display in the calendar panel
let monthNumber = (new Date().getMonth());
let monthNames = ["TAMMI", "HELMI", "MAALIS", "HUHTI", "TOUKO", "KESÄ", "HEINÄ", "ELO", "SYYS", "LOKA", "MARRAS", "JOULU"];
let monthName = monthNames[monthNumber];

//Here we get the current day to display in the active meeting
var days = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
var d = new Date();
var dayName = days[d.getDay()];

//This maps the meetings.json data into mapAppointmentData variable.
//mapAppointmentData only gets three attributes from each item in the meetings.json file
//These are starting time, ending time and the title of the meetings
const mapAppointmentData = meetings => ({
    ...meetings,
    startDate: meetings.StartTime.slice(0,10) +' '+ meetings.StartTime.slice(11,16),
    endDate: meetings.EndTime.slice(0,10) + ' ' + meetings.EndTime.slice(11,16),
    title: meetings.Subject,
    Participants: meetings.Participants,
    Organizer: meetings.Organizer,
});

const Appointment = ({
    children, onClick: openInformation, style, ...restProps
  }) => (
    <Appointments.Appointment
      {...restProps}
      style={{
        ...style,
        backgroundColor: '#FC0',
      }}
     // onClick={() => this.openInformation}
    >
      {children}
    </Appointments.Appointment>
  );

class ScheduleStart extends Component {


    //Constructor of the component
    constructor(props) {
        super(props);
        this.openInformation = this.openInformation.bind(this);
        //this.backToCalendar = this.backToCalendar.bind(this);
        this.state = {
        data: meetings,
        openInfo: false
        };
      }

      //Here we get 'copy' the meetings.json data into data variable
    componentDidMount() {
        fetch(meetings)
        .then(response => response.json())
        .then(({ data }) => {
          setTimeout(() => {
            this.setState({
              data
            });
          }, 600);
        })
    }

    //This function will change the openInfo variable which is used
    //to decide which view is shown on the panel
    openInformation = () => {
        this.setState({
            openInfo: true
        })
    }

    backToCalendar = () => {
      this.setState({
        openInfo: false
      })
    }


    render() {
        const { data } = this.state;
        

        //Here the formattedData gets the items from mapAppointmentData
        //and data variables and puts them in a new array
        const formattedData = data
         ? data.map(mapAppointmentData) : [];


        if(this.state.openInfo === false)
        {
            return (
                <div className="ScheduleStart">
                  <h2 className="ConferenceRoom">
                      CONFERENCE ROOM <button className="OpenActiveMeeting" onClick={this.openInformation}>Meeting View</button>
                  </h2>
                  <h2 className="Month">{monthName}</h2>
                    <MuiThemeProvider>
                    <Scheduler
                        data={formattedData}              > 
                    <ViewState currentDate='2018-03-03' />         
                    <DayView startDate="2018-03-03" startDayHour={8} endDayHour={20}/>
                    <Appointments appointmentComponent={Appointment}/>
                    </Scheduler>
                    </MuiThemeProvider>
                </div>
                    )
        }

        if(this.state.openInfo === true)
        {
            return (
                <div className="ScheduleOpen">
                    <button className="BackToStart" onClick={this.backToCalendar}>
                    {meetings.map((entry, index) => {
                            if(entry.Participants !== null)
                            {
                               return '<---'+entry.Subject;
                           }
                          })}
                    </button>
                    <img src={Clock} />
                          {dayName}
                          {meetings.map((entry, index) => {
                            if(entry.Participants !== null)
                            {
                               return ' ' + entry.StartTime.slice(8,10) + '.' + entry.StartTime.slice(5,7) + '.' + entry.StartTime.slice(0,4);
                           }
                          })}

                    <br /> <img src={Clock} />

                      {meetings.map((entry, index) => {
                            if(entry.Participants !== null)
                            {
                               return entry.StartTime.slice(11,16) + '-' +entry.EndTime.slice(11,16);
                           }
                          })}

                    <br /> <img src={Person} /> PARTICIPANTS
                    <br />
                    
                    {meetings.map((entry, index) => {
                      if(entry.Participants !== null)
                      {
                        return(
                          <ul>
                            
                              {entry.Participants.map(participant => (
                              <span><ul><li>{participant.Name}</li></ul><br/></span>
                              ))}
                            
                          </ul>
                        )
                      }
                    })}

                    <br /> <img src={Description} />  DESCRIPTION
                </div>
            )
        }


    }
}
export default ScheduleStart;