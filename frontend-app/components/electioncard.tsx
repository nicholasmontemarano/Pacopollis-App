"use client";

import { useEffect, useState } from "react";
import { Button } from "@/components/ui/button";
import Link from "next/link";
import {
  Card,
  CardContent,
  CardDescription,
  CardFooter,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import axios from "axios";

export const ElectionCard = ({
  _electionID, _electionName, _voted }:
  {
    _electionID: number;
    _electionName: string;
    _voted: boolean;
  }) => {
  const [electionID, setElectionID] = useState(_electionID)
  const [electionName, setElectionName] = useState(_electionName);
  const [voted, setVoted] = useState(_voted);
  const [adminState, setAdminState] = useState(0);

  useEffect(() => {
    // Admin Check
    const loggedInUser = sessionStorage.getItem('user');
    if (loggedInUser != null) {
      try {
        const url =
          "http://localhost:5014/api/IsAdmin?voterid=" + parseInt(loggedInUser);
        const response = axios
          .get(url)
          .then((response) => {
            if (response.data == true) {
              setAdminState(1);
            } else {
              setAdminState(0);
            }
          })
          .catch((err) => console.log(err));
      } catch (error) {
        console.error(error);
      }
    }

    // Check if they have voted
    try {
      const loggedInUser = sessionStorage.getItem("user");
      const url =
        "http://localhost:5014/api/IfVoted?voter_id=" + loggedInUser + "&election_id=" + electionID;
      const response = axios
        .get(url)
        .then((response) => {
          setVoted(response.data);
        })
        .catch((err) => console.log(err));
    } catch (error) {
      console.error(error);
    }
  }, []);

  function AdminViewButton() {
    return (
      <Button asChild id="adminButton" variant="ghost" className="px-5">
        <Link href={`/voteview?electionid=${electionID}`}>Admin View</Link>
      </Button>
    );
  }

  function VoteButton() {
    return (
      <Button asChild id="voteButton" className="px-10">
        <Link href={`/ballot?electionid=${electionID}&voted=${voted}`}>Vote</Link>
      </Button>
    );
  }
  function ViewVoteButton() {
    return (
      <Button asChild id="voteButton" className="px-10">
        <Link href={`/ballot?electionid=${electionID}&voted=${voted}`}>View Your Votes</Link>
      </Button>
    );
  }

  return (
    <div className="relative w-full">
      <Card className="flex flex-row justify-between items-baseline">
        <CardHeader>
          <CardTitle>Start Date: {electionName}</CardTitle>
        </CardHeader>
        <CardContent>
          {adminState === 1 && (
            AdminViewButton()
          )}
          {voted === true && (
            ViewVoteButton()
          )}
          {voted === false && (
            VoteButton()
          )}
        </CardContent>
      </Card>
    </div>
  );
};
