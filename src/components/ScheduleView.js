import React, { Component } from 'react';
import ScheduleStart from './ScheduleStart';
import ScheduleOpen from './ScheduleOpen'

//This component decides which schedule view is active.
//These views are the calendar itself
//and the information of a certain meeting

class ScheduleView extends Component {
    render() {
        return (
            <div className="ScheduleView">
                <ScheduleStart />
            </div>
        )
    }
}
export default ScheduleView;