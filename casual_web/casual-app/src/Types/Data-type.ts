export  interface MikeJob {
    id: string;
    title: string;
    location: string;
    urlDetail: string;
    description: string;
    postedAt: string;
    salary: string;
    category: string;
}


export interface StatItem{
    name: string;
    count: number;
}

export interface JobTrends{
    date: string;
    newJobsCount: number;
}

export interface DashboardData{
    dailyJobs: MikeJob[];
    jobsByCategory: StatItem[];
    jobsByLocation: StatItem[];
    jobTrends: JobTrends[];
}