import React, { Component } from 'react';
import authService from './api-authorization/AuthorizeService'

export class FetchData extends Component {
    static displayName = FetchData.name;

    constructor(props) {
        super(props);
        this.state = { students: [], loading: true };
    }

    componentDidMount() {
        this.populateWeatherData();
    }

    static renderStudentsTable(students) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Last Name</th>
                        <th>First Mid Name </th>
                        <th> Enrollment Date </th>
                    </tr>
                </thead>
                <tbody>
                    {students.map(student =>
                        <tr key={student.id} >
                            <td>{student.lastName}</td>
                            <td>{student.firstMidName}</td>
                            <td>{new Date(student.enrollmentDate).toISOString()}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : FetchData.renderStudentsTable(this.state.students);

        return (
            <div>
                <h1 id="tabelLabel"> Student List </h1>
                <p>This component demonstrates fetching data from the server.</p>
                {contents}
            </div>
        );
    }

    async populateWeatherData() {
        const token = await authService.getAccessToken();
        const response = await fetch('students', {
            headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
        });
        const data = await response.json();
        this.setState({ students: data, loading: false });
    }
}
