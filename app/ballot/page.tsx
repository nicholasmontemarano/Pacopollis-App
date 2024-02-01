"use client";

import { useEffect, useState } from "react";
import { NavBubble } from "../navbar";
import { useSearchParams } from "next/navigation";
import axios from "axios";
import { Label } from "@radix-ui/react-label";
import { BallotList } from "@/components/ballotlist";
import BItem from "../interfaces/iteminterface";
import Ballot from "../interfaces/ballotinterface";
import Vote from "../interfaces/votesinterface";
import { Logout } from "@/components/logoutbutton";
import { BallotView } from "@/components/ballotview";
import { M_PLUS_1_Code } from "next/font/google";
import { setMaxListeners } from "events";
import { Button } from "@/components/ui/button";
import Link from "next/link";

export default function BallotPage() {
  const searchParams = useSearchParams();
  const [electionID, setElectionID] = useState(searchParams.get("electionid"));
  const [voted, setVoted] = useState(searchParams.get("voted"));
  const [ballotList, setBallotList] = useState();
  const [isBusy, setIsBusy] = useState(true);
  const [user, setUser] = useState('');
  const [voteList, setVoteList] = useState();

  // User has voted
  function displayVotes() {
    // fill the ballotData list with the list of ballotItems
    const voteData: Vote[] = [];
    const url =
      "http://localhost:5014/api/VoteView?voter_id=" +
      user +
      "&election_id=" +
      electionID;
    const response = axios
      .get(url)
      .then((response) => {
        if (response.data != null) {
          var x = 0;
          response.data.forEach(
            (ballot: { id: number, name: string, items: { title: string, votedFor: boolean, id: number }[] }) => {
              x++;
              const newVote: Vote = {
                voteID: x,
                ballotID: ballot.id,
                ballotIssue: ballot.name,
                itemID: 0,
                itemTitle: "",
              };
              ballot.items.forEach((option) => {
                if (option.votedFor == true) {
                  newVote.itemID = option.id;
                  newVote.itemTitle = option.title;
                }
              });
              voteData.push(newVote);
            }
          );
          setVoteList(voteData);
          setIsBusy(false);
        }
      })
      .catch((err) => console.log(err));
    return (
      <BallotView votes={voteData} />
    );
  }

  // User has not voted
  function displayBallot() {
    const loggedInUser = sessionStorage.getItem("user");
    const ballotData: Ballot[] = [];
    // fill the ballotData list with the list of ballotItems
    const url =
      "http://localhost:5014/api/VoteView?voter_id=" +
      loggedInUser +
      "&election_id=" +
      electionID;
    const response = axios
      .get(url)
      .then((response) => {
        if (response.data != null) {
          response.data.forEach(
            (ballot: { id: number, name: string, items: BItem[] }) => {
              // add an instance to the ballotList
              const newBallot: Ballot = {
                id: ballot.id,
                ballotIssue: ballot.name,
                items: [],
              };
              ballot.items.forEach((option) => {
                const newOption: BItem = { id: option.id, title: option.title };
                newBallot.items.push(newOption);
              });
              ballotData.push(newBallot);
            }
          );
          setBallotList(ballotData);
          setIsBusy(false);
        }
      })
      .catch((err) => console.log(err));
    return (
      <BallotList ballots={ballotData} />
    )
  }

  useEffect(() => {
    // Check for a user logon
    const loggedInUser = sessionStorage.getItem("user");
    setUser(loggedInUser);
    if (loggedInUser == "-1" || loggedInUser == null || loggedInUser == undefined) {
      alert("No user logon");
      location.href = "http://localhost:3000/";
    }
    setIsBusy(false);
  }, []);

  if (!isBusy) {
    return (
      <main className="h-screen w-screen flex justify-center items-center bg-slate-100">
        <div className="shadow-xl p-4 bg-white rounded-xl">
          <h1 className="font-semibold text-3xl"> Ballot </h1>

          {voted === "false" && (
            displayBallot()
          )}
          {voted === "true" && (
            displayVotes()
          )}
          <div className=" p-4 flex justify-center items-center">
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


}