"use client";

import { useEffect, useState } from "react";

import { useSearchParams } from "next/navigation";
import axios from "axios";
import Election from '@/app/interfaces/electionInterface'
import { ElectionCard } from "./electioncard";

interface ElectionProps {
  elections: Election[];
}

export const ElectionList: React.FC<ElectionProps> = ({ elections }: ElectionProps) => {
  const [electionList, setElectionList] = useState(elections);

  return (
    <div>
      {electionList.map((item) => (
        <ElectionCard key={item.id}
          _electionID={item.id}
          _electionName={item.electionName}
          _voted={item.voted}
        />
      ))}
    </div>
  );
};