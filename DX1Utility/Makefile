###########################################################################################################
### C:/LOCAL/STORE/DEV/PROJECTS/RTabs/Makefile ### SCRIPT_TAG (200729:18h:36) #############################
###########################################################################################################
:cd %:h|make view_project
# VARS {{{
 TOP		= $(LOCAL)/STORE/DEV/HARDWARE
 APPDIR		= DX1Utility
 LS		= ls.exe
 JAR		= jar.exe
#}}}
:cd %:h|update|only|set columns=999|vert terminal make GITHUB
################ GITHUB {{{
 GITHUB:
	@(echo "[$@]";\
	   FILES="Profiles/Global.xml `find .  -type f | egrep -v '\<(Profiles|txt|log|html|vim|tags|BAK|_gsdata_|ini|lnk|vs|obj|git|suo|exe|dll|pdb|application|lnk)\>' | sed -e 's/^\.\/*//' -e 's/ /\n/'`";\
	   LINKS=`find R* -type l | sed -e 's/^\.\/*//' -e 's/ /\n/'`;\
	\
	LINKS=`echo "$$LINKS" | grep -v '.cs'`;\
	LINKS_COUNT=`echo "$$LINKS" | wc -l`;\
	echo "$$LINKS_COUNT LINK files";\
	echo "$$LINKS";\
	\
	FILES_MISC=`echo "$$FILES" | grep -v '.cs'`;\
	FILES_MISC_COUNT=`echo "$$FILES_MISC" | wc -l`;\
	echo "$$FILES_MISC_COUNT MISC files";\
	echo "$$FILES_MISC";\
	\
	FILES_CS=`echo "$$FILES" | grep    '.cs'`;\
	FILES_CS_COUNT=`echo "$$FILES_CS" | wc -l`;\
	echo "$$FILES_CS_COUNT cs files";\
	echo "$$FILES_CS";\
	\
	echo "ARCHIVE";\
	    DATE=`date '+%y%m%d'`;\
	    ARCHIVE="$(APPDIR)_$$DATE.zip";\
	    rm -f                    ../GITHUB/$$ARCHIVE;\
	    $(JAR)   Mcf             ../GITHUB/$$ARCHIVE $$FILES;\
	    $(LS)         -lt        ../GITHUB/*.zip | head -2;\
	    $(JAR)   tvf             ../GITHUB/$$ARCHIVE;\
	\
	echo "[$@ DONE]";\
	)

#}}}

# vim: noexpandtab ts=8 sw=8
