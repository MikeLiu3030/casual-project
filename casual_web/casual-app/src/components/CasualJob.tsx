import {useState} from 'react';
import React, { useCallback, useRef } from 'react';


import { apiGet } from '../Api';
import type { MikeJob } from '../Types/MikeJob-type';
import "../Css/casualjob.css" ;

import { toPng } from 'html-to-image';
import axios from 'axios';

interface JobCardProps {
    job:MikeJob;
}

const JobCard : React.FC<JobCardProps> = ({ job }) => {
    const cardRef = useRef<HTMLDivElement>(null);

    const onButtonClick = useCallback(() => {
        if (cardRef.current === null) return;
        
        toPng(cardRef.current, { 
            cacheBust: true,
            canvasWidth:1080,
            canvasHeight:1440,
            backgroundColor:'#f4ff7f6' 
        
        })
        .then((dataUrl) => {
            const link = document.createElement('a')
            link.download = job.id
            link.href = dataUrl
            link.click()
        })
        .catch((err) => {
            console.log(err)
        })


    }, [job])

    return (
    <>
    <div className='job-card-wrapper' >
        <div className='jobDev' ref={cardRef}>
            <div className='job-item'>
                <strong>Title</strong>
                <div >{job.title}</div>

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

    </>)


}

export default function CasualJob(){

    const [jobs, setJobs] = useState<MikeJob[]>([]);
    const [errorMsg, setErrorMsg] = useState<string | null>(null);
    const [targetDate, setTargetDate] = useState<string>('')
    const handleGetdata =async () => {
        if (!targetDate){
            setErrorMsg("Please select date!")
            return;
        }
        console.log(targetDate);
        
        try{
            
            const result = await apiGet<MikeJob[]>(`api/GetJob/GetJob?date=${targetDate}`);

            setJobs(result);
            setErrorMsg(null);
        } catch(err){
            if(axios.isAxiosError(err)){
                if (err.response){
                    const serverMessage = typeof err.response.data === 'string'?
                    err.response.data : (err.response.data.message);
                    setErrorMsg(serverMessage);
                } else if (err.request){
                    setErrorMsg("can't connect server, please check internet!")
                } else {
                    setErrorMsg(`Request configuration error:${err.message}`)
                }
            } else {
                setErrorMsg('Unknown Error!')
            }
        }
    }

    return (
    <>
        <section>
            <div className='top-controls'>
                <input 
                    type="date"
                    className='date-input'
                    value={targetDate}
                    onChange={(e) => setTargetDate(e.target.value)} />
                <button className='btn' onClick={handleGetdata}>Get Data</button>
            </div>

            {errorMsg?
            <div>
                {errorMsg}
            </div>
            
            :<div>
                <ul className="job-list" >
                {
                    jobs.map((job) => (
                        <JobCard key={job.id} job={job} />
                    ))
                }
                </ul>
            </div>

            }

        </section>
        <hr className='my-divider'/>
    </>)
}