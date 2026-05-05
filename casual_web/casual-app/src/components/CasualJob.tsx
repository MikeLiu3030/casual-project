import { useState } from 'react';
import React, { useCallback, useRef } from 'react';

import { apiGet } from '../Api';
import type { MikeJob } from '../Types/MikeJob-type';
import "../Css/casualjob.css";

import { toPng } from 'html-to-image';
import axios from 'axios';

interface JobCardProps {
    job: MikeJob;
}

const JobCard: React.FC<JobCardProps> = ({ job }) => {
    // Reference to the specific card DOM element for image capture
    const cardRef = useRef<HTMLDivElement>(null);

    // Memoized function to convert HTML to PNG and trigger a download
    const onButtonClick = useCallback(() => {
        if (cardRef.current === null) return;

        toPng(cardRef.current, {
            cacheBust: true, // Prevents browser caching issues
            canvasWidth: 1080,
            canvasHeight: 1440,
            backgroundColor: '#f4ff7f6'
        })
            .then((dataUrl) => {
                // Create a virtual link to trigger the browser download
                const link = document.createElement('a');
                link.download = `${job.id}.png`;
                link.href = dataUrl;
                link.click();
            })
            .catch((err) => {
                console.error("Image generation failed:", err);
            });

    }, [job]);

    return (
        <>
            <div className='job-card-wrapper'>
                {/* ref={cardRef} tells html-to-image exactly what to capture */}
                <div className='jobDev' ref={cardRef}>
                    <div className='job-item'>
                        <strong>Title</strong>
                        <div>{job.title}</div>

                        <strong>Location</strong>
                        <div>{job.location}</div>

                        <strong>Url Detail</strong>
                        <div className='url-detail'>{job.urlDetail}</div>

                        <strong>Description</strong>
                        <div>{job.description}</div>

                        <strong>Posted At</strong>
                        <div>{job.postedAt?.split('T')[0]}</div>
                    </div>
                </div>
                <div className='imageSaveButton'>
                    <button className='btn' onClick={onButtonClick}>Save Image</button>
                </div>
            </div>
        </>
    );
}

export default function CasualJob() {
    // State management for jobs list, error messages, and the date filter
    const [jobs, setJobs] = useState<MikeJob[]>([]);
    const [errorMsg, setErrorMsg] = useState<string | null>(null);
    const [targetDate, setTargetDate] = useState<string>('');

    const handleGetdata = async () => {
        // Validation: Prevent API calls if no date is selected
        if (!targetDate) {
            setErrorMsg("Please select a date!");
            return;
        }

        try {
            // Fetch data using the selected date as a query parameter
            const result = await apiGet<MikeJob[]>(`api/GetJob/GetJob?date=${targetDate}`);

            setJobs(result);
            setErrorMsg(null); // Clear errors on success
        } catch (err) {
            // Comprehensive Axios error handling
            if (axios.isAxiosError(err)) {
                if (err.response) {
                    // Scenario 1: Server responded with an error (e.g., 404, 500)
                    const serverMessage = typeof err.response.data === 'string' ?
                        err.response.data : (err.response.data.message);
                    setErrorMsg(serverMessage || "Server Error");
                } else if (err.request) {
                    // Scenario 2: Request sent but no response received (Network/CORS)
                    setErrorMsg("Cannot connect to server, please check your internet!");
                } else {
                    // Scenario 3: Error during request setup
                    setErrorMsg(`Request configuration error: ${err.message}`);
                }
            } else {
                setErrorMsg('An unexpected error occurred!');
            }
        }
    }

    return (
        <>
            <section>
                <div className='top-controls'>
                    {/* Controlled input for the date picker */}
                    <input
                        type="date"
                        className='date-input'
                        value={targetDate}
                        onChange={(e) => setTargetDate(e.target.value)}
                        style={{ marginRight: '10px' }} // Spacing added here
                    />
                    <button className='btn' onClick={handleGetdata}>Get Data</button>
                </div>

                {/* Conditional Rendering: Show error message OR the job list */}
                {errorMsg ?
                    <div className="error-display">
                        {errorMsg}
                    </div>
                    :
                    <div>
                        <ul className="job-list">
                            {jobs.map((job) => (
                                <JobCard key={job.id} job={job} />
                            ))}
                        </ul>
                    </div>
                }
            </section>
            <hr className='my-divider' />
        </>
    );
}