# Kashkeshet
--------------------------------
# Git Conventions
--------------------------------

# Branch Names:
1. The branch names should be lowercased.
2. If the branch names is constructed from a few words, words should be seperated by by a dash

# Header Types:
1. Addition: Add something to the solution. Could be any kind of asset or code component.
2. Deletion: Delete something in the solution.
3. Modification: Modify anything in the solution.
4. Implementation: Add a component that implements something.
5. Fix: Fix anything like a merge conflict or a bug or an error.
6. SolutionModification: Modify the structure of any of the projects or the solution.
7: BranchConclusion: Final commit for a certain branch. Usually used to upload unsaved changes to a csproj or sln file.

# Commit Structure:
[H]eaderType: The commit description.

# Rules:
1. Headers should always start with a capital letter.
2. The commit description should always start with a captial letter.
3. Names of files/code-components/projects should be written like the original.

--------------------------------
# Kashkeshet Packets Protocol
--------------------------------
# Packet Structure:
TYPE <Headers-length> \r\n
<Headers> \r\n
\r\n
<Content>

# Packet Types:
1. REQ - Request. Both Server and client can send requests.
2. RES - Response to a request. Usually an error to a request, but can also be acknowledgement. Both Server and client can send requests.
3. PUSH - An event that the server is pushing to the client. Could be a new message, notification, anything.

# Packet Rules:
1. Every header inside the packets should end with \r\n (like HTTP).
2. The headers section should end with another \r\n apart from the last header's line end.
3. A packet can contain a Content section after the headers section. In order for the receiver to be able to use the content data, the packet
	headers should contain at least these two headers:
	- Content-type: (text/image/audio/etc)
	- Content-Length: The length of the content the receiver should expect.
4. Each word in a Header should start with a capital letter.
5. If a header name contains more than one word, the words should be seperated by a dash.

# Headers:
- Date: The exact date & time the packet has been sent.
- Request-Type: The type of the request. 
	For example:
	- send/broadcast
	- send/direct
	- send/group
	- create/group
- Error-Type: The type of error.
	For example:
	- user-does-not-exist
	- group-invalid
- Event-Type: The type of push event received.
	For example:
	- new-message/(general/group/direct)
	- user-connect
	- user-disconnect
- Sender: The name of the user that sent the packet.
	