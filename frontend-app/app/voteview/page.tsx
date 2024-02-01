"use client"

import { NavBubble } from "../navbar";
import { AdminView } from "@/components/adminvoteview";
import { useSearchParams } from "next/navigation";
import { useState, useEffect } from "react";
import { Label } from "@radix-ui/react-label";
import axios from "axios";
import AdminItem from "../interfaces/admininterface";
import AItem from "../interfaces/aiteminterface";
import { Logout } from "@/components/logoutbutton";
import { Button } from "@/components/ui/button";
import Link from "next/link";

export default function VoteView() {
  //Find the electionID
  const searchParams = useSearchParams();
  const [electionID, setElectionID] = useState(searchParams.get('electionid'));
  const [isBusy, setIsBusy] = useState(false);
  const ballotData: AdminItem[] = [];
  const [ballotList, setBallotList] = useState(ballotData);

  useEffect(() => {
    // Check for a user logon
    const loggedInUser = sessionStorage.getItem("user");
    if (loggedInUser == "-1" || loggedInUser == null || loggedInUser == undefined) {
      alert("No user logon");
      location.href = "http://localhost:3000/";
    } else { // User has logged on
      // fill the ballotData list with the list of ballotItems
      const url =
        "http://localhost:5014/api/AdminView?election_id=" +
        electionID;
      const response = axios
        .get(url)
        .then((response) => {
          if (response.data != null && ballotData.length == 0) {
            response.data.forEach(
              (ballot: { id: number, name: string, items: any[] }) => {
                // add an instance to the ballotList
                const newBallot: AdminItem = {
                  id: ballot.id,
                  ballotIssue: ballot.name,
                  totalVotes: 0,
                  items: [],
                };
                ballot.items.forEach((option) => {
                  const newOption: AItem = { id: option.id, title: option.title, votes: option.adminVoteTotal, votePercent: 0 };
                  newBallot.totalVotes += option.adminVoteTotal;
                  newBallot.items.push(newOption);
                });
                ballotData.push(newBallot);
                setBallotList(ballotData);
              }
            );
          }
        })
        .catch((err) => console.log(err));
    }
    setIsBusy(false);
  }, []);

  if (isBusy) {
    return (
      <Label>Loading...</Label>
    );
  }

  return (
    <main className="h-screen w-screen flex justify-center items-center bg-slate-100">
      <div className="w-[50%] shadow-xl p-4 bg-white rounded-xl">
        <h1 className="font-semibold text-3xl"> Election Results </h1>
        <AdminView ballots={ballotData} />
        <div className="flex justify-center items-center">
          {/* <NavBubble /> */}
          <Button asChild variant="outline">
            <Link href="http://localhost:3000/dashboard">Back</Link>
          </Button>
          <Logout />
        </div>
      </div>
    </main>
  );
}

