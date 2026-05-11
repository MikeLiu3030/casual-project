// src/components/CasualJob.tsx
import React, { useState, useCallback, useRef, useMemo } from 'react';
import { apiGet } from '../Api';
import type { MikeJob, DashboardData } from '../Types/Data-type';
import "../Css/casualjob.css";

import { toPng } from 'html-to-image';
import axios from 'axios';
import { 
    PieChart, Pie, LineChart, Line, BarChart, Bar, 
    XAxis, YAxis, CartesianGrid, Tooltip, ResponsiveContainer, Legend
} from 'recharts';

const COLORS = ['#0088FE', '#00C49F', '#FFBB28', '#FF8042', '#8884d8', '#82ca9d', '#ffc658'];

const JobCard: React.FC<{ job: MikeJob }> = ({ job }) => (
    <div className='job-card-wrapper'>
        <div className='jobDev'>
            <div className='job-item'>
                <div><strong>{job.title}</strong></div>
                <div className="job-salary">{job.salary}</div>
                <div className="job-meta">{job.location} | {job.category}</div>
            </div>
        </div>
    </div>
);

export default function CasualJob() {
    const [dashboardData, setDashboardData] = useState<DashboardData | null>(null);
    const [errorMsg, setErrorMsg] = useState<string | null>(null);
    const [targetDate, setTargetDate] = useState<string>('');
    const [initial, setInitial] = useState<boolean>(true);

    const jobsListRef = useRef<HTMLDivElement>(null)
    const chartsPosterRef = useRef<HTMLDivElement>(null);

    const handleGetdata = async () => {
        if (!targetDate) {
            setErrorMsg("Please select a date!");
            return;
        }
        try {
            const result = await apiGet<DashboardData>(`api/GetJob/GetJob?date=${targetDate}`);
            setDashboardData(result);
            setInitial(false);
            setErrorMsg(null);
        } catch (err) {
            if (axios.isAxiosError(err)) {
                const serverMessage = typeof err.response?.data === 'string' 
                    ? err.response.data : err.response?.data?.message;
                setErrorMsg(serverMessage || "Server Error");
            } else {
                setErrorMsg('An unexpected error occurred!');
            }
        }
    }

    const chartData = useMemo(() => {
        return dashboardData?.jobsByCategory.map((item, index) => ({
                ...item,
                fill:COLORS[index % COLORS.length]
            })) || [];
    }, [dashboardData])

    const saveImage = async (ref: React.RefObject<HTMLDivElement | null>, fileName: string) =>{
        if (ref.current === null) return;
        try{
            const dataUrl = await toPng(ref.current, {
                cacheBust: true,
                backgroundColor:'#ffffff',
                pixelRatio: 1,
                // width:1080,
                // height:1440,
                canvasWidth:1080,
                canvasHeight:1440,
            });
            const link = document.createElement('a');
            link.download = fileName;
            link.href = dataUrl;
            link.click();
        } catch (err) {
            console.error(`Export ${fileName} failed:`, err)
        }
    };


    const downloadPoster = useCallback( async () => {
        await saveImage(jobsListRef, `Jobs_List_${targetDate}.png`);
        setTimeout(async () => {
            await saveImage(chartsPosterRef, `Market_Report_${targetDate}.png`)
        }, 500);
    }, [targetDate]);

    return (
        <section className="dashboard-section">
            <div className='top-controls-container'>
                <input type="date" className='date-input' value={targetDate} onChange={(e) => setTargetDate(e.target.value)} />
                <button className='btn btn-primary' onClick={handleGetdata}>Get Data</button>
                {dashboardData && (
                    <button className='btn btn-success' onClick={downloadPoster}>Download Poster</button>
                )}
            </div>

            {errorMsg ? (
                <div className="error-display">{errorMsg}</div>
            ) : initial ? (
                <div className="initial-prompt"><strong>Please Select a Date!</strong></div>
            ) : dashboardData ? (
                <div className="content-item">                    
                    <div className="jobDisplay-item" ref={jobsListRef}>
                        <div className="poster-header">
                            <h2>Jobs List </h2>
                            <p>Date: {targetDate}</p>
                        </div>

                        <div className="job-list">
                            {dashboardData.dailyJobs?.map((job) => <JobCard key={job.id} job={job} />)}
                        </div>
                    </div>

                    <div className="statistic-poster" ref={chartsPosterRef}>
                        <div className="poster-header">
                            <h2>Casual Job Market Report</h2>
                            <p>Date: {targetDate}</p>
                        </div>

                        <div className="chart-wrapper">   
                            <h4>Jobs by Category</h4>                         
                            <ResponsiveContainer width="100%" height={200}>
                                <PieChart>
                                    <Pie 
                                        data={chartData} 
                                        dataKey="count" nameKey="name" 
                                        cx="50%" 
                                        cy="30%" 
                                        outerRadius={35} 
                                        innerRadius={15}
                                        label={({ percent=0 }) => `${(percent * 100).toFixed(0)}%`}
                                    >
                                    </Pie>
                                    <Tooltip />
                                    <Legend 
                                        layout="vertical" 
                                        verticalAlign="top" 
                                        align="right"
                                        wrapperStyle={{ fontSize:'10px' }}/>
                                </PieChart>
                            </ResponsiveContainer>                            
                        </div>

                        <div className="chart-wrapper">
                            <h4>Jobs by Location</h4>                            
                            <ResponsiveContainer width="100%" height={250}>
                                <BarChart 
                                    data={[...dashboardData.jobsByLocation].sort((a,b) => a.count - b.count)} 
                                    layout="vertical" 
                                    margin={{ top: 5, right: 10, left: 10, bottom: 5 }}
                                >
                                    <CartesianGrid strokeDasharray="3 3" horizontal={false} />
                                    <XAxis type="number" />
                                    <YAxis dataKey="name" type="category" width={70} tick={{fontSize: 12}} />
                                    <Tooltip cursor={{fill: 'transparent'}} />
                                    <Bar dataKey="count" fill="#00C49F" radius={[0, 4, 4, 0]} barSize={20} />
                                </BarChart>
                            </ResponsiveContainer>                            
                        </div>

                        <div className="chart-wrapper">
                            <h4>14-Day Posting Trend</h4>
                            <ResponsiveContainer width="100%" height={250}>
                                <LineChart data={dashboardData.jobTrends} margin={{ top: 10, right: 30, left: 0, bottom: 0 }}>
                                    <CartesianGrid strokeDasharray="3 3" vertical={false} />
                                    <XAxis 
                                        dataKey="date" 
                                        tickFormatter={(t) => new Date(t).toLocaleDateString('en-NZ', { month: 'short', day: 'numeric' })}
                                        tick={{fontSize: 11}}
                                    />
                                    <YAxis tick={{fontSize: 12}} />
                                    <Tooltip />
                                    <Line type="monotone" dataKey="newJobsCount" stroke="#8884d8" strokeWidth={3} dot={{ r: 4 }} activeDot={{ r: 6 }} />
                                </LineChart>
                            </ResponsiveContainer>
                            
                        </div>                                          
                    </div>
                </div>
            ) : null}
        </section>
    );
}